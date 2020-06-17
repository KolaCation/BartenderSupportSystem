using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace BartenderSupportSystem.Server.Helpers
{
    public sealed class IdentityProfileService  : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            UserManager<IdentityUser> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);
            var claimsPrincipal = await _claimsFactory.CreateAsync(user);
            var claims = claimsPrincipal.Claims.ToList();

            var claimsDB = await _userManager.GetClaimsAsync(user);

            var mappedClaims = new List<Claim>();

            foreach (var claim in claimsDB)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    mappedClaims.Add(new Claim(JwtClaimTypes.Role, claim.Value));
                }
                else
                {
                    mappedClaims.Add(claim);
                }
            }

            claims.AddRange(mappedClaims);

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);
            context.IsActive = user != null;
        }
    }
}
