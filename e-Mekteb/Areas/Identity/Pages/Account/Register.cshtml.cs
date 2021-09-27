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
using EmailSender;
using System.Data;

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
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterModel(
            UserManager<AplicationUser> userManager,
            SignInManager<AplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,e_MektebDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            this.roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Ime i Prezime")]
            public string ImeiPrezime { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = "Pogrešan format email adrese")]
            [Display(Name = "Korisničko ime")]
            public string Email { get; set; }

            [Required]
            public string Mjesto { get; set; }

           


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Lozinka:")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potvrdi lozinku:")]
            [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju")]
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
            //Trenutni ulogirani korisnik
            var ulogiraniUser = HttpContext.User.Identity.Name;

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AplicationUser { ImeiPrezime=Input.ImeiPrezime,UserName = Input.Email, Email = Input.Email, NazivMjesta = Input.Mjesto };
                user.AplicationUserId = user.Id;

                //Provjera dali postoji korisnik sa ovim imenom i prezimenom u bazi
                var korisnici =_userManager.Users.ToList();
                foreach(var noviKorisnik in korisnici)
                {
                    var ulogiraniUserName= await _userManager.FindByNameAsync(ulogiraniUser);

                    if (noviKorisnik.ImeiPrezime == user.ImeiPrezime)
                    {

                        return RedirectToPage();


                       
                    }



                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account",
                    //    new {area="Identity" ,user = user.Id, code = code }, Request.Scheme);
                    //_logger.Log(LogLevel.Warning, callbackUrl);


                    if (user.Email == "senad.mandzic1984@gmail.com")
                    {
                        if(!await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            IdentityRole identityRole = new IdentityRole
                            {
                                Name = "Admin"
                            };
                            IdentityResult results=await roleManager.CreateAsync(identityRole);
                            if (results.Succeeded)
                            {
                                {
                                    await _userManager.AddToRoleAsync(user, "Admin");
                                    return RedirectToAction("ListRole", "Administration");
                                }
                                
                            }
                            
                        }
                                    
                    }
                    else
                    {
                        var vjeroucitelj = await _userManager.FindByNameAsync(ulogiraniUser);
                        
                        if (await _userManager.IsInRoleAsync(vjeroucitelj, "Vjeroucitelj"))
                        {
                            var ucenik = await _userManager.FindByEmailAsync(Input.Email);
                            var vjerouciteljUcenik = new VjerouciteljUcenik
                            {
                                VjerouciteljId = vjeroucitelj.Id,
                                UcenikId = ucenik.Id,
                                UserName = ucenik.Email
                            };
                            
                            _context.Add(vjerouciteljUcenik);
                            await _context.SaveChangesAsync();
                        }

                    }
                            
                            
                            

                           







                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code,returnUrl=returnUrl},
                        protocol: Request.Scheme);


                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                    SendEmail sendEmail = new SendEmail(Input.Email,callbackUrl,Input.Password);
                    _ = sendEmail.Execute();

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if(_signInManager.IsSignedIn(User) && User.IsInRole("Admin")) 
                        {

                            return RedirectToAction("ListRole", "Administration");
                        }
                        else
                            if(_signInManager.IsSignedIn(User) && User.IsInRole("Vjeroucitelj"))
                        {
                            return RedirectToAction("ListUsers", "Vjeroucitelj");

                        }
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(returnUrl);
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
