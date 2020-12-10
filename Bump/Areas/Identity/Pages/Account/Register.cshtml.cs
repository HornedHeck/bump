using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Localization.Errors;
using Bump.Resources.Localization.Strings;
using Bump.Services.Email;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

// ReSharper disable MemberCanBePrivate.Global

namespace Bump.Areas.Identity.Pages.Account {

    [AllowAnonymous]
    public class RegisterModel : PageModel {

        private readonly EmailSender _email;
        private readonly IStringLocalizer< CommonErrors > _localizer;
        private readonly ILogger< RegisterModel > _logger;

        private readonly SignInManager< BumpUser > _signInManager;
        private readonly UserManager< BumpUser > _userManager;

        public RegisterModel(
            UserManager< BumpUser > userManager ,
            SignInManager< BumpUser > signInManager ,
            ILogger< RegisterModel > logger ,
            IStringLocalizer< CommonErrors > localizer , EmailSender email ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
            _email = email;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList< AuthenticationScheme > ExternalLogins { get; set; }

        public async Task OnGetAsync( string returnUrl = null ) {
            ReturnUrl = returnUrl;
            ExternalLogins = ( await _signInManager.GetExternalAuthenticationSchemesAsync() ).ToList();
        }

        public async Task< IActionResult > OnPostAsync( string returnUrl = null ) {
            returnUrl ??= Url.Action( "Index" , "Home" );
            ExternalLogins = ( await _signInManager.GetExternalAuthenticationSchemesAsync() ).ToList();

            if( !ModelState.IsValid ) return Page();

            var user = new BumpUser {
                UserName = Input.Login ,
                VisibleName = Input.VisibleName ,
                Email = Input.Email
            };
            IdentityResult result;
            try {
                result = await _userManager.CreateAsync( user , Input.Password );
            }
            catch( DbUpdateException ) {
                result = IdentityResult.Failed( new IdentityError {
                    Description = _localizer [ "DuplicateVisibleName" ]
                } );
            }

            if( result.Succeeded ) {
                _logger.LogInformation( "User created a new account with password." );

                await _userManager.AddToRoleAsync( user , AuthConstants.User );

                var code = await _userManager.GenerateEmailConfirmationTokenAsync( user );
                code = WebEncoders.Base64UrlEncode( Encoding.UTF8.GetBytes( code ) );
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail" ,
                    null ,
                    new {area = "Identity" , userId = user.Id , code , returnUrl} ,
                    Request.Scheme );


                await _email.SendWelcomeEmail( user.Email , callbackUrl );

                return RedirectToPage( "RegisterConfirmation" , new {email = Input.Email , returnUrl} );
            }

            foreach( var error in result.Errors ) ModelState.AddModelError( string.Empty , _localizer [ error.Code ?? error.Description ] );

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel {

            [LRequired]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Login" )]
            public string Login { get; set; }

            [LRequired]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Email" )]
            public string Email { get; set; }

            [LRequired]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "VisibleName" )]
            public string VisibleName { get; set; }

            [LRequired]
            [LStringLength( 100 , MinimumLength = 6 )]
            [DataType( DataType.Password )]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Password" )]
            public string Password { get; set; }

            [DataType( DataType.Password )]
            [Display( Name = "Confirmation" )]
            [LConfirmation( "Password" )]
            public string ConfirmPassword { get; set; }

        }

    }

}