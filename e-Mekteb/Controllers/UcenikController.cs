using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace e_Mekteb.Controllers
{
    public class UcenikController : Controller
    {
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext context;
        public UcenikController(UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var user = userManager.Users.Where(u=>u.Email==userEmail);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {id}";
                return NotFound();
            }
            else
            {
                var model = new UcenikViewModel
                {
                    ImeiPrezime=user.ImeiPrezime,
                    NazivMjesta=user.NazivMjesta,
                    Ulica=user.NazivMjesta,
                    PostanskiBroj=user.PostanskiBroj,
                    DatumRodenja=user.DatumRodenja,
                    Spol=user.Spol,
                    Starost=user.Starost,
                    ImeOca=user.ImeOca,
                    Email=user.Email

                };
                
                var enumSpol = Enum.GetValues(typeof(Spol)).Cast<Spol>().Select(v => v.ToString()).ToList();
                ViewData["Spol"] = new SelectList(enumSpol);
                
                return View(model);
            }
           

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UcenikViewModel model, string id)
        {

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {id}";
                return NotFound();
            }
            else
            {
                
                
                
                    user.ImeiPrezime = model.ImeiPrezime;
                    user.NazivMjesta = model.NazivMjesta;
                    user.Ulica = model.Ulica;
                    user.PostanskiBroj = model.PostanskiBroj;
                    user.DatumRodenja = model.DatumRodenja;
                    user.Starost = model.Starost;
                    user.Spol = model.Spol;
                    user.ImeOca = model.ImeOca;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }

                return View(model);

            }









        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {id}";
                return NotFound();

            }
            else
            {
                var model = new UcenikViewModel
                {
                    ImeiPrezime = user.ImeiPrezime,
                    Email = user.Email,
                    NazivMjesta = user.NazivMjesta,
                    Ulica = user.Ulica,
                    PostanskiBroj = user.PostanskiBroj,
                    Spol=user.Spol,
                    Starost=user.Starost,
                    ImeOca=user.ImeOca,
                    DatumRodenja=user.DatumRodenja
                
                };
                return View(model);
            }
        }
            
            

              
    }



}
