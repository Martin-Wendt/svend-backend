using Microsoft.AspNetCore.Authorization;
using SitrepAPI.Entities;

namespace SitrepAPI.AuthorizationHandlers
{
    /// <summary>
    /// auth handler for HasAccesRequirement.
    /// checks whether you are the creator of a case or you have 'manager' or 'operator' roles.
    /// <returns>Task</returns>
    /// </summary>
    public class HasAccessHandler : AuthorizationHandler<HasAccessRequirement, Case>
    {
        /// <summary>
        /// Validation of requirement
        /// </summary>
        /// <param name="context">Auth Context</param>
        /// <param name="requirement">Requirement to pass</param>
        /// <param name="resource">Case to validate against</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentNullException">Null value provided to method</exception>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessRequirement requirement, Case resource)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.User.IsInRole("Manager"))
            {
                context.Succeed(requirement);
            }
            if (context.User.IsInRole("Operator"))
            {
                context.Succeed(requirement);
            }

            if (resource is not null)
            {
                if (context?.User?.Identity?.Name == resource.UserId)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
