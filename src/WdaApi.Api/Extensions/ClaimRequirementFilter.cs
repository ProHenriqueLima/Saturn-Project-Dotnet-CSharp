using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SaturnApi.Api.Extensions
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {

        public ClaimRequirementAttribute(string profile) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim("", profile) };
        }
        public class ClaimRequirementFilter : IAuthorizationFilter
        {
            readonly Claim _claim;
            private readonly IProfileRepository _profileRepository;
            private readonly IUserRepository _userRepository;
            private readonly UserManager<ApplicationUser> _userManager;

            public ClaimRequirementFilter(Claim claim,
                IProfileRepository profileRepository, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
            {

                _claim = claim;
                _profileRepository = profileRepository;
                _userRepository = userRepository;
                _userManager = userManager;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {

                try
                {
                    string userName = context.HttpContext.User.Identity.Name;

                    string[] permissions = _claim.Value.Split(",");

                    var user = Task.Run(() => getPermissionIdAsync(context.HttpContext.User.Identity.Name)).Result;


                    context.Result = new StatusCodeResult(403);

                }
                catch
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            private async Task<ApplicationUser> getPermissionIdAsync(string userName)
            {
                return await _userManager.FindByNameAsync(userName);
            }
        }
    }
}


