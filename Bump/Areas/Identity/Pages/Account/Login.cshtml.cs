using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Localization.Strings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Bump.Areas.Identity.Pages.Account {

    [AllowAnonymous]
    public class LoginModel : PageModel {

        private readonly IStringLocalizer< InputModel > _localizer;
        private readonly ILogger< LoginModel > _logger;
        private readonly SignInManager< BumpUser > _signInManager;

        private readonly UserManager< BumpUser > _userManager;

        public LoginModel(
            SignInManager< BumpUser > signInManager ,
            ILogger< LoginModel > logger ,
            UserManager< BumpUser > userManager ,
            IStringLocalizer< InputModel > localizer
        ) {
            _userManager = userManager;
            _localizer = localizer;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList< AuthenticationScheme > ExternalLogins { get; private set; }

        public string ReturnUrl { get; private set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync( string returnUrl = null ) {
            if( !string.IsNullOrEmpty( ErrorMessage ) ) ModelState.AddModelError( string.Empty , ErrorMessage );

            returnUrl ??= Url.Action( "Index" , "Home" );

            await HttpContext.SignOutAsync( IdentityConstants.ExternalScheme );

            ExternalLogins = ( await _signInManager.GetExternalAuthenticationSchemesAsync() ).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task< IActionResult > OnPostAsync( string returnUrl = null ) {
            returnUrl ??= Url.Action( "Index" , "Home" );

            if( !ModelState.IsValid ) return Page();
            var result = await _signInManager.PasswordSignInAsync(
                Input.Login ,
                Input.Password ,
                Input.RememberMe ,
                false
            );

            if( result.Succeeded ) {
                _logger.LogInformation( "User logged in." );

                return LocalRedirect( returnUrl );
            }

            if( result.RequiresTwoFactor ) return RedirectToPage( "./LoginWith2fa" , new {ReturnUrl = returnUrl , Input.RememberMe} );

            if( result.IsLockedOut ) {
                _logger.LogWarning( "User account locked out." );

                return RedirectToPage( "./Lockout" );
            }

            ModelState.AddModelError( string.Empty , _localizer [ "Error" ] );

            return Page();

        }

        public class InputModel {

            [LRequired]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Login" )]
            public string Login { get; set; }

            [LRequired]
            [DataType( DataType.Password )]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Password" )]
            public string Password { get; set; }

            [Display( Name = "Remember" )]
            public bool RememberMe { get; set; }

        }

    }

}