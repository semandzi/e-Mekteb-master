using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace e_Mekteb.Controllers
{
    [Authorize(Roles = "Ucenik")]

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
            var user = userManager.Users.Where(u => u.Email == userEmail).ToList();
            return View(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"Ne postoji korisnik sa ovim id brojem: {id}";
                return NotFound();
            }
            else
            {
                var model = new UcenikViewModel
                {
                    ImeiPrezime = user.ImeiPrezime,
                    NazivMjesta = user.NazivMjesta,
                    Ulica = user.NazivMjesta,
                    PostanskiBroj = user.PostanskiBroj,
                    DatumRodenja = user.DatumRodenja,
                    Spol = user.Spol,
                    BrojMobitela = user.BrojMobitela,
                    ImeiPrezimeRoditelja = user.ImeiPrezimeRoditelja,
                    Email = user.Email

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
                ViewBag.Error = $"Ne postoji korisnik sa ovim id brojem: {id}";
                return NotFound();
            }
            else
            {


                user.ImeiPrezime = model.ImeiPrezime;
                user.NazivMjesta = model.NazivMjesta;
                user.Ulica = model.Ulica;
                user.PostanskiBroj = model.PostanskiBroj;
                user.DatumRodenja = model.DatumRodenja;
                user.BrojMobitela = model.BrojMobitela;
                user.Spol = model.Spol;
                user.ImeiPrezimeRoditelja = model.ImeiPrezimeRoditelja;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Details");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }

                return View(model);

            }









        }
        
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userName = HttpContext.User.Identity.Name;
            var korisnik = await userManager.FindByEmailAsync(userName);
            var user = await userManager.FindByIdAsync(korisnik.Id);
            ViewBag.UcenikId = korisnik.Id;
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {korisnik.Id}";
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
                    Spol = user.Spol,
                    BrojMobitela = user.BrojMobitela,
                    ImeiPrezimeRoditelja = user.ImeiPrezimeRoditelja,
                    DatumRodenja = user.DatumRodenja

                };
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MojeBiljeske()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await userManager.FindByEmailAsync(email);
            var biljeske = context.Biljeske.Where(b => b.AplicationUserId == user.AplicationUserId).ToList();
            var aktivnosti = context.Aktivnosti.ToList();
            var tempBiljeske = new List<Biljeska>();
            var tempAktivnosti = new List<Aktivnost>();
            foreach (var aktivnost in aktivnosti)
            {
                foreach (var biljeska in biljeske)
                {
                    if (biljeska.AktivnostId == aktivnost.AktivnostId)
                    {
                        tempBiljeske.Add(biljeska);
                        if (tempAktivnosti.Contains(aktivnost))
                        {
                            continue;

                        }
                        else
                        {
                            tempAktivnosti.Add(aktivnost);

                        }
                    }


                }

            }
            var model = new UcenikoveBiljeskeUcenikView
            {
                UcenikoveAktivnosti = tempAktivnosti,
                UcenikoveBiljeske = tempBiljeske
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MojePrisutnosti()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await userManager.FindByEmailAsync(email);
            var prisutnosti = context.Prisutnosti.Where(b => b.AplicationUserId == user.AplicationUserId).ToList();
            var aktivnosti = context.Aktivnosti.ToList();
            var tempPrisutnosti = new List<Prisutnost>();
            var tempAktivnosti = new List<Aktivnost>();
            foreach (var aktivnost in aktivnosti)
            {
                foreach (var prisutnost in prisutnosti)
                {
                    if (prisutnost.AktivnostId == aktivnost.AktivnostId)
                    {
                        tempPrisutnosti.Add(prisutnost);
                        if (tempAktivnosti.Contains(aktivnost))
                        {
                            continue;

                        }
                        else
                        {
                            tempAktivnosti.Add(aktivnost);

                        }
                    }


                }

            }
            var model = new UcenikovePrisutnostiUcenikView
            {
                UcenikoveAktivnosti = tempAktivnosti,
                UcenikovePrisutnosti = tempPrisutnosti
            };

            return View(model);


        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> MojeObavijesti()
        {

            if (User.IsInRole("Ucenik"))
            {
                var username = HttpContext.User.Identity.Name;
                var ucenik = await userManager.FindByNameAsync(username);
                var ucenikId = ucenik.Id;
                var tempObavijesti = new List<Obavijest>();
                var tempVjeroucitelji = new List<AplicationUser>();

                var vjerouciteljucenik = context.VjerouciteljUcenik.Where(p => p.UcenikId == ucenikId).ToList();
                foreach (var vjeroucitelj in vjerouciteljucenik)
                {
                    var vjerouciteljObavijesti = context.Obavijesti.Where(o => o.VjerouciteljId == vjeroucitelj.VjerouciteljId).ToList();
                    foreach (var obavijest in vjerouciteljObavijesti)
                    {
                        if (obavijest.VjerouciteljId == vjeroucitelj.VjerouciteljId)
                        {
                            if (tempObavijesti.Contains(obavijest))
                            {
                                continue;
                            }
                            else
                            {
                                tempObavijesti.Add(obavijest);

                            }
                        }
                        var vjerouciteljUser = await userManager.FindByIdAsync(vjeroucitelj.VjerouciteljId);

                        if (tempVjeroucitelji.Contains(vjerouciteljUser))
                        {
                            continue;
                        }
                        else
                        {
                            tempVjeroucitelji.Add(vjerouciteljUser);

                        }


                    }
                }



                var model = new Obavijesti
                {
                    obavijesti = tempObavijesti,
                    VjerouciteljiNaObavijestima = tempVjeroucitelji

                };

                return View(model);
            }

            return View();








        }




    }
}




