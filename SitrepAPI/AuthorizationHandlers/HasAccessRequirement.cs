using Microsoft.AspNetCore.Authorization;

namespace SitrepAPI.AuthorizationHandlers
{
    /// <summary>
    /// HasAccess requirement, used in combination with 'HasAccessHandler' 
    /// <see cref="HasAccessHandler"/>
    /// </summary>
    public class HasAccessRequirement : IAuthorizationRequirement
    {
    }
}
