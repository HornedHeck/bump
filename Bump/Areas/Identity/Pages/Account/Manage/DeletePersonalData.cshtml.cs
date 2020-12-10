using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Localization.Errors;
using Bump.Resources.Localization.Strings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Bump.Areas.Identity.Pages.Account.Manage {

    public class DeletePersonalDataModel : PageModel {

        private readonly UserManager< BumpUser > _userManager;
        private readonly SignInManager< BumpUser > _signInManager;
        private readonly ILogger< DeletePersonalDataModel > _logger;
        private readonly IStringLocalizer< Localization.Messages.Identity > _localizer;

        public DeletePersonalDataModel(
            UserManager< BumpUser > userManager ,
            SignInManager< BumpUser > signInManager ,
            ILogger< DeletePersonalDataModel > logger ,
            IStringLocalizer< Localization.Messages.Identity > localizer
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {

            [LRequired]
            [DataType( DataType.Password )]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Password" )]
            public string Password { get; set; }

        }

        public bool RequirePassword { get; set; }

        public async Task< IActionResult > OnGet() {
            var user = await _userManager.GetUserAsync( User );
            if( user == null ) {
                return this.AccessDenied();
            }

            RequirePassword = await _userManager.HasPasswordAsync( user );

            return Page();
        }

        public async Task< IActionResult > OnPostAsync() {
            var user = await _userManager.GetUserAsync( User );
            if( user == null ) {
                return this.AccessDenied();
            }

            RequirePassword = await _userManager.HasPasswordAsync( user );
            if( RequirePassword ) {
                if( Input?.Password == null || !await _userManager.CheckPasswordAsync( user , Input.Password ) ) {
                    if( Input?.Password != null ) {
                        ModelState.AddModelError( string.Empty , _localizer [ "WrongPassword" ] );
                    }

                    return Page();
                }
            }

            var result = await _userManager.DeleteAsync( user );
            var userId = await _userManager.GetUserIdAsync( user );
            if( !result.Succeeded ) {
                throw new InvalidOperationException( $"Unexpected error occurred deleting user with ID '{userId}'." );
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation( "User with ID '{UserId}' deleted themselves." , userId );

            return Redirect( AuthConstants.LoginPath );
        }

    }

}