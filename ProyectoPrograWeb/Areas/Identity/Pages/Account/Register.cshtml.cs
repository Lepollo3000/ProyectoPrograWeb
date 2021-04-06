using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ProyectoPrograWeb.Models;

namespace ProyectoPrograWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string UserLastname { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //CAPITALIZE FIRST LETTER OF EACH WORD
                Input.UserName = Input.UserName.ToLower();
                Input.UserLastname = Input.UserLastname.ToLower();
                Input.UserName =
                    string.Join(" ", Input.UserName.Split(' ').ToList()
                            .ConvertAll(word =>
                                    word.Substring(0, 1).ToUpper() + word.Substring(1)
                            )
                    );
                Input.UserLastname =
                    string.Join(" ", Input.UserLastname.Split(' ').ToList()
                            .ConvertAll(word =>
                                    word.Substring(0, 1).ToUpper() + word.Substring(1)
                            )
                    );

                var user = new ApplicationUser { 
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    FirstName = Input.UserName,
                    LastName = Input.UserLastname
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //AQUI VA EL HTML MAS BONITO OVO
                    var emailHtml = $@"
                    <div style='background-color: #fdfdfd;'>
                        <div style='
                            margin: 8%;
                            margin-bottom: 0;
                            color: #294661;
                            display: flex;
                            align-items: center;
                            justify-content: center;'>
                            <img src='data:image/png;base64,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' />
                            <h2>Paws On Ur Heart</h2>
                        </div>

                        <div style='
                            display: flex;
                            align-items: center;
                            justify-content: center;
                            border-top: solid 10px #fb6542;
                            border-bottom: solid 10px #fb6542;
                            border-right: solid 1px #dbdbdb;
                            border-left: solid 1px #dbdbdb;
                            margin: 8%;
                            margin-top: 0;
                            margin-bottom: 1%'>
                            <div style='color: #294661 !important; margin-top: 5%; margin-bottom: 5%;'>
                                <h2 style='margin-bottom: 5%'>Let's confirm your email address.</h2>

                                <hr style='border: 1px solid;' />

                                <h5 style='margin-bottom: 5%'>By clicking on the following link, you are confirming your email address.</h5>
                                <div style='display: flex;
                                    align-items: center;
                                    justify-content: center;'>
                                    <a style='
                                        box-sizing: border-box;
                                        border-color: #fb6542;
                                        font-weight: 400;
                                        text-decoration: none;
                                        display: inline-block;
                                        margin: 0;
                                        color: #ffffff;
                                        background-color: #fb6542;
                                        border: solid 1px #c95236;
                                        border-radius: 2px;
                                        font-size: 14px;
                                        padding: 12px 45px;'
                                       href='{HtmlEncoder.Default.Encode(callbackUrl)}'>
                                        Confirm email adress
                                    </a>
                                </div>
                            </div>
                        </div>

                        <div style='display: flex; align-items: center; justify-content: center;'>
                            © Paws On Ur Heart | 2021
                        </div>
                    </div>
                    ";

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email", emailHtml);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
