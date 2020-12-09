using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Localization.Errors;
using Bump.Resources.Strings;
using Bump.Services.Email;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Bump.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BumpUser> _signInManager;
        private readonly UserManager<BumpUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IStringLocalizer<CommonErrors> _localizer;
        private readonly EmailSender _email;

        public RegisterModel(
            UserManager<BumpUser> userManager,
            SignInManager<BumpUser> signInManager,
            ILogger<RegisterModel> logger,
            IStringLocalizer<CommonErrors> localizer, EmailSender email)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _localizer = localizer;
            _email = email;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [LRequired]
            [Display(ResourceType = typeof(CommonStrings), Name = "Login")]
            public string Login { get; set; }

            [LRequired]
            [Display(ResourceType = typeof(CommonStrings), Name = "Email")]
            public string Email { get; set; }

            [LRequired]
            [Display(ResourceType = typeof(CommonStrings), Name = "VisibleName")]
            public string VisibleName { get; set; }

            [LRequired]
            [LStringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(ResourceType = typeof(CommonStrings), Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmation")]
            [LConfirmation("Password")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new BumpUser
                {
                    UserName = Input.Login,
                    VisibleName = Input.VisibleName,
                    Email = Input.Email
                };
                IdentityResult result;
                try
                {
                    result = await _userManager.CreateAsync(user, Input.Password);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    result = IdentityResult.Failed(new IdentityError
                    {
                        Description = _localizer["DuplicateVisibleName"]
                    });
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, AuthConstants.User);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new {area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl},
                        protocol: Request.Scheme);


                    await _email.SendWelcomeEmail(user.Email, callbackUrl);
                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    // $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    // {
                    return RedirectToPage("RegisterConfirmation", new {email = Input.Email, returnUrl = returnUrl});
                    // }

                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    // return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, _localizer[error.Code ?? error.Description]);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}