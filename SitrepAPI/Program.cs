using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SitrepAPI.AuthorizationHandlers;
using SitrepAPI.DbContexts;
using SitrepAPI.ProblemDetails;
using SitrepAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager Configuration = builder.Configuration;
//Remove CORS restrictions.
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
}));


//Problem detail configuration
builder.Services
    .AddProblemDetails(ProblemDetailConfiguration.ProblemDetailConfigurationExecution)

    .AddControllers(setup =>
    {
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status415UnsupportedMediaType));
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status422UnprocessableEntity));
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
        setup.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status403Forbidden));

        setup.Filters.Add(new ProducesAttribute("application/json"));
        setup.Filters.Add(new ConsumesAttribute("application/json"));



        setup.ReturnHttpNotAcceptable = true;

    }).AddNewtonsoftJson()
    .AddProblemDetailsConventions()
    .AddJsonOptions(c => c.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)
    .ConfigureApiBehaviorOptions(setup =>
    {
        setup.InvalidModelStateResponseFactory = context =>
        {
            // create a problem details object
            var problemDetailFactory = context.HttpContext.RequestServices.GetRequiredService<Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory>();
            var problemDetails = problemDetailFactory.CreateValidationProblemDetails(
                    context.HttpContext,
                    context.ModelState);

            problemDetails.Detail = "See the errors field for details.";
            problemDetails.Instance = context.HttpContext.Request.Path;

            // find out which status code to use
            var actionExecutingContext =
                  context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

            // if there are modelstate errors & all keys were correctly
            // found/parsed we're dealing with validation errors
            if ((context.ModelState.ErrorCount > 0) &&
                (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
            {
                problemDetails.Type = "Input validation error";
                problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                problemDetails.Title = "One or more validation errors occurred.";

                return new UnprocessableEntityObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            }

            // if one of the keys wasn't correctly found / couldn't be parsed
            // we're dealing with null/unparsable input
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "One or more errors on input occurred.";
            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

// Authentication and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.Authority = Configuration["Authentication:Authority"];
    options.Audience = Configuration["Authentication:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
        RoleClaimType = "https://sitrep.dk//roles"
    };
});

builder.Services.AddAuthorization(options =>
{
    /* Auth gennem policies
    *  info på link
    *  https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/october/cutting-edge-policy-based-authorization-in-asp-net-core 
    */
    options.AddPolicy("HasAccess", policy => policy.Requirements.Add(new HasAccessRequirement()));
});

//Adds dbcontext
builder.Services.AddDbContext<SitrepDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection"), opt => opt.EnableRetryOnFailure()));

//Swagger
builder.Services.AddEndpointsApiExplorer();
//Swagger config
var contactUri = new UriBuilder("www.donutabuse.dk");
contactUri.Port = 80;
contactUri.Scheme = "http";
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Sitrep API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "support@sitrep.dk",
            Name = "Sitrep Support",
            Url = contactUri.Uri
        },
        Description = "API for Sitrep",
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "License: Donut Abuse",
            Url = contactUri.Uri
        }
    });

    config.AddSecurityDefinition("openid", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    {"openid", "Open Id" }
                },
                AuthorizationUrl = new Uri(Configuration["Authentication:Authority"] + "authorize?audience=" + Configuration["Authentication:Audience"])
            }
        },
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "openid"
            }
        }

        ] = new[] { "api1" }
    });
    
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    config.IncludeXmlComments(xmlCommentsFullPath);
});
//In memory cache
builder.Services.AddMemoryCache();

//Gets automapper profiles in the current project
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services

builder.Services.AddScoped<ICaseRepository, CaseRepository>();
builder.Services.AddScoped<ICaseImageRepository, CaseImageRepository>();
builder.Services.AddScoped<ICaseLogRepository, CaseLogRepository>();

builder.Services.AddTransient<IPropertyMappingService, PropertyMappingService>();
builder.Services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
builder.Services.AddSingleton<IUserInformationService, UserInformationService>();
builder.Services.AddSingleton<IAuthorizationHandler, HasAccessHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseCors();


app.UseSwaggerUI(c =>
{
    c.OAuthClientId(Configuration["Authentication:ClientId"]);
    c.OAuthScopes(Configuration["Authentication:Audience"]);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
