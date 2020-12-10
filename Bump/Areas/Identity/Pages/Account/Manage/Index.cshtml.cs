using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Localization.Strings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Bump.Areas.Identity.Pages.Account.Manage {

    public class IndexModel : PageModel {

        private readonly IStringLocalizer< Localization.Messages.Identity > _localizer;
        private readonly SignInManager< BumpUser > _signInManager;

        private readonly UserManager< BumpUser > _userManager;

        public IndexModel(
            UserManager< BumpUser > userManager ,
            SignInManager< BumpUser > signInManager ,
            IStringLocalizer< Localization.Messages.Identity > localizer
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
        }

        [Display( ResourceType = typeof( CommonStrings ) , Name = "Login" )]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        private async Task LoadAsync( BumpUser user ) {
            var userName = await _userManager.GetUserNameAsync( user );
            var visibleName = await _userManager.GetVisibleNameAsync( user );

            Username = userName;

            Input = new InputModel {
                VisibleName = visibleName
            };
        }

        public async Task< IActionResult > OnGetAsync() {
            var user = await _userManager.GetUserAsync( User );

            if( user == null ) return this.AccessDenied();

            await LoadAsync( user );

            return Page();
        }

        public async Task< IActionResult > OnPostAsync() {
            var user = await _userManager.GetUserAsync( User );

            if( user == null ) return this.AccessDenied();

            if( !ModelState.IsValid ) {
                await LoadAsync( user );

                return Page();
            }

            var visibleName = await _userManager.GetVisibleNameAsync( user );
            if( Input.VisibleName != visibleName ) {
                var setVisibleNameResult = await _userManager.SetVisibleNameAsync( user , Input.VisibleName );
                if( !setVisibleNameResult.Succeeded ) {
                    StatusMessage = _localizer [ "ProfileUpdateError" ].Value;

                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync( user );
            StatusMessage = _localizer [ "ProfileUpdated" ].Value;

            return RedirectToPage();
        }

        public class InputModel {

            [LRequired]
            [Display( ResourceType = typeof( CommonStrings ) , Name = "VisibleName" )]
            public string VisibleName { get; set; }

        }

    }

}