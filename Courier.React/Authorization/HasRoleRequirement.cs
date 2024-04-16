using Microsoft.AspNetCore.Authorization;

namespace Courier.React.Authorization
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string[] Roles { get; }

        public HasRoleRequirement(string[] roles, string issuer)
        {
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
