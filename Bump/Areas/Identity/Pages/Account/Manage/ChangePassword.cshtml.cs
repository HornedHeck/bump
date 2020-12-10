using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Areas.Identity.Pages.Account.Manage;
using Bump.Resources.Localization.Strings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Bump.Areas.Identity.Pages.Account.Manage {

    public class ChangePasswordModel : PageModel {

        private readonly ILogger< ChangePasswordModel > _logger;
        private readonly SignInManager< BumpUser > _signInManager;
        private readonly UserManager< BumpUser > _userManager;
        private readonly IStringLocalizer< Localization.Messages.Identity > _localizer;

        public ChangePasswordModel(
            UserManager< BumpUser > userManager ,
            SignInManager< BumpUser > signInManager ,
            ILogger< ChangePasswordModel > logger ,
            IStringLocalizer< Localization.Messages.Identity > localizer
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task< IActionResult > OnGetAsync() {
            var user = await _userManager.GetUserAsync( User );

            if( user == null ) return this.AccessDenied();

            var hasPassword = await _userManager.HasPasswordAsync( user );

            if( !hasPassword ) return RedirectToPage( "./SetPassword" );

            return Page();
        }

        public async Task< IActionResult > OnPostAsync() {
            if( !ModelState.IsValid ) return Page();

            var user = await _userManager.GetUserAsync( User );

            if( user == null ) return this.AccessDenied();

            var changePasswordResult =
                await _userManager.ChangePasswordAsync( user , Input.OldPassword , Input.NewPassword );
            if( !changePasswordResult.Succeeded ) {
                foreach( var error in changePasswordResult.Errors )
                    ModelState.AddModelError( string.Empty , error.Description );

                return Page();
            }

            await _signInManager.RefreshSignInAsync( user );
            _logger.LogInformation( "User changed their password successfully." );
            StatusMessage = _localizer [ "PasswordChanged" ].Value;

            return RedirectToPage();
        }

        public class InputModel {

            [LRequired]
            [DataType( DataType.Password )]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "Password" )]
            public string OldPassword { get; set; }

            [LRequired]
            [LStringLength( 100 , MinimumLength = 6 )]
            [DataType( DataType.Password )]
            [Display( Name = "NewPassword" )]
            public string NewPassword { get; set; }

            [DataType( DataType.Password )]
            [Display( Name = "NewConfirmation" )]
            [LConfirmation( "NewPassword" )]
            public string ConfirmPassword { get; set; }

        }

    }

}