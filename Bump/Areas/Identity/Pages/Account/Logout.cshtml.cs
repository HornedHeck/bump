using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Bump.Areas.Identity.Pages.Account {

    [AllowAnonymous]
    public class LogoutModel : PageModel {

        private readonly ILogger< LogoutModel > _logger;

        private readonly SignInManager< BumpUser > _signInManager;

        public LogoutModel( SignInManager< BumpUser > signInManager , ILogger< LogoutModel > logger ) {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet() { }

        public async Task< IActionResult > OnPost( string returnUrl = null ) {

            returnUrl ??= Url.Page( "Login" );

            await _signInManager.SignOutAsync();
            _logger.LogInformation( "User logged out." );

            if( returnUrl != null ) return LocalRedirect( returnUrl );

            return RedirectToPage();
        }

    }

}