using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bump.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<BumpUser> _userManager;
        private readonly SignInManager<BumpUser> _signInManager;

        public IndexModel(
            UserManager<BumpUser> userManager,
            SignInManager<BumpUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData] public string StatusMessage { get; set; }

        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Visible Name")]
            public string VisibleName { get; set; }
        }

        private async Task LoadAsync(BumpUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var visibleName = (await _userManager.GetClaimsAsync(user))
                .First(it => it.Type == ClaimTypes.GivenName)
                .Value;

            Username = userName;

            Input = new InputModel
            {
                VisibleName = visibleName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var visibleName = await _userManager.GetVisibleNameAsync(user);
            if (Input.VisibleName != visibleName)
            {
                var setVisibleNameResult = await _userManager.SetVisibleNameAsync(user, Input.VisibleName);
                if (!setVisibleNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set visible name.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}