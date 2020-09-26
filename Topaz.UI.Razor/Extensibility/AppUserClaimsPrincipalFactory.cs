using Microsoft.AspNetCore.Identity;
using Topaz.Common.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Topaz.UI.Razor.Extensibility
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        public AppUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(
                userManager,
                roleManager,
                optionsAccessor
            )
        { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            if (user.PublisherId.HasValue)
            {
                identity.AddClaim(new Claim("PublisherId", user?.PublisherId.ToString() ?? string.Empty));
            }
            return identity;
        }
    }
}