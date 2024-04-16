using Microsoft.AspNetCore.Authorization;

namespace Courier.React.Authorization
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        const string roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == roleClaimType && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var roles = context.User.FindFirst(c => c.Type == roleClaimType && c.Issuer == requirement.Issuer).Value.Split(' ');

            // Succeed if the scope array contains the required scope
            if (roles.Any(s => requirement.Roles.Contains(s)))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
