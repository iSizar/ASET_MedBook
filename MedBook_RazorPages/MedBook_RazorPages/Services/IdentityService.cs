using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedBook_RazorPages.Services
{
    public class IdentityService : IIndentityService
    {
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(UserManager<IdentityUser> userManager) {
            _userManager = userManager;
        }

        /*private AuthenticationResult GenerateAuthentificationResultForUserAsync*/

    /*    public async Task<AuthenticationResult> LoginWithFacebookAsync(string acccessToken)
        {
            var validatedTokenResult = await _facebookAuthService.ValidateAccessTokenAsync(acccessToken);
            if (!validatedTokenResult.Data.IsValid)
            {
                return new AuthenticationResult
                {
                };
            }

            var userInfo = await _facebookAuthService.GetUserInfoResult(acccessToken);
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null) { 
            }

            return await GenerateAuthentificationResultForUserAsync(user);
        }*/
    }
}
