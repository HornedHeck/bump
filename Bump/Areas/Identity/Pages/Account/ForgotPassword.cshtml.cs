using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Bump.Areas.Identity.Pages.Account {

    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel {

        private readonly UserManager< BumpUser > _userManager;

        public ForgotPasswordModel( UserManager< BumpUser > userManager ) {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task< IActionResult > OnPostAsync() {
            if( !ModelState.IsValid ) return Page();

            var user = await _userManager.FindByEmailAsync( Input.Email );

            if( user == null || !await _userManager.IsEmailConfirmedAsync( user ) ) // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage( "./ForgotPasswordConfirmation" );

            // For more information on how to enable account confirmation and password reset please 
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync( user );
            code = WebEncoders.Base64UrlEncode( Encoding.UTF8.GetBytes( code ) );
            var callbackUrl = Url.Page(
                "/Account/ResetPassword" ,
                null ,
                new {area = "Identity" , code} ,
                Request.Scheme );

            return RedirectToPage( "./ForgotPasswordConfirmation" );

        }

        public class InputModel {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

        }

    }

}