using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using e_Mekteb.Models;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using e_Mekteb.ApDbContext;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace e_Mekteb.Areas.Identity.Pages.Account
{
    [Authorize(Roles ="Admin,Vjeroucitelj")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly e_MektebDbContext _context;

        public RegisterModel(
            UserManager<AplicationUser> userManager,
            SignInManager<AplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,e_MektebDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Korisničko ime:")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Medžlis")]
            [ForeignKey("Medzlis")]
            public Medzlis Medzlis { get; set; }
            public int MedzlisId { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Lozinka:")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potvrdi lozinku:")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ViewData["MedzlisId"] = new SelectList(_context.Users, "MedzlisId", "Naziv");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AplicationUser { UserName = Input.Email, Email = Input.Email,MedzlisId=Input.MedzlisId};
                user.AplicationUserId = user.Id;
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var username = HttpContext.User.Identity.Name;
                    var vjeroucitelj = await _userManager.FindByNameAsync(username);
                    var ucenik = await _userManager.FindByEmailAsync(Input.Email);
                    var vjerouciteljUcenik = new VjerouciteljUcenik
                    {
                        VjerouciteljId = vjeroucitelj.Id,
                        UcenikId=ucenik.Id
                    };

                    _context.Add(vjerouciteljUcenik);
                    await _context.SaveChangesAsync();
                   





                        _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if(_signInManager.IsSignedIn(User) && User.IsInRole("Admin")) 
                        {

                            return RedirectToAction("ListUsers", "Administration");
                        }
                        else
                            if(_signInManager.IsSignedIn(User) && User.IsInRole("Vjeroucitelj"))
                        {
                            return RedirectToAction("ListUsers", "Vjeroucitelj");

                        }
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
