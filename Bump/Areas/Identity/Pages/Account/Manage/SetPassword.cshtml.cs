using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Localization.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Bump.Areas.Identity.Pages.Account.Manage
{
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<BumpUser> _userManager;
        private readonly SignInManager<BumpUser> _signInManager;
        private readonly IStringLocalizer<Localization.Messages.Identity> _localizer;
        private readonly IStringLocalizer<CommonErrors> _errors;

        public SetPasswordModel(
            UserManager<BumpUser> userManager,
            SignInManager<BumpUser> signInManager,
            IStringLocalizer<Localization.Messages.Identity> localizer,
            IStringLocalizer<CommonErrors> errors
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
            _errors = errors;
        }

        [BindProperty] public InputModel Input { get; set; }

        [TempData] public string StatusMessage { get; set; }

        public class InputModel
        {
            [LRequired]
            [LStringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "NewPassword")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "NewConfirmation")]
            [LConfirmation("NewPassword")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return this.AccessDenied();
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return this.AccessDenied();
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, _errors[error.Code]);
                }

                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _localizer["PasswordSet"];

            return RedirectToPage();
        }
    }
}