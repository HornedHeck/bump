using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bump.Areas.Identity.Pages.Account.Manage {

    public class PersonalDataModel : PageModel {

        private readonly UserManager< BumpUser > _userManager;

        public PersonalDataModel( UserManager< BumpUser > userManager ) => _userManager = userManager;

        public async Task< IActionResult > OnGet() {
            var user = await _userManager.GetUserAsync( User );

            return user == null ? this.AccessDenied() : Page();

        }

    }

}