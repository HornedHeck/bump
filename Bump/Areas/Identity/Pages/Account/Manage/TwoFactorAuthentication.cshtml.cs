using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

// ReSharper disable MemberCanBePrivate.Global

namespace Bump.Areas.Identity.Pages.Account.Manage {

    public class TwoFactorAuthenticationModel : PageModel {

        private readonly ILogger< TwoFactorAuthenticationModel > _logger;
        private readonly SignInManager< BumpUser > _signInManager;

        private readonly UserManager< BumpUser > _userManager;

        public TwoFactorAuthenticationModel(
            UserManager< BumpUser > userManager ,
            SignInManager< BumpUser > signInManager ,
            ILogger< TwoFactorAuthenticationModel > logger ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        [BindProperty]
        public bool Is2faEnabled { get; set; }

        public bool IsMachineRemembered { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task< IActionResult > OnGet() {
            var user = await _userManager.GetUserAsync( User );

            if( user == null ) return this.AccessDenied();

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync( user ) != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync( user );
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync( user );
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync( user );

            return Page();
        }

        public async Task< IActionResult > OnPost() {
            var user = await _userManager.GetUserAsync( User );

            if( user == null ) return this.AccessDenied();

            await _signInManager.ForgetTwoFactorClientAsync();
            StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";

            return RedirectToPage();
        }

    }

}