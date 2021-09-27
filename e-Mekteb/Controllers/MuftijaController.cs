using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers
{
    [Authorize(Roles = "Muftija")]
    public static class FilterPodaci
    {
        public static int SkolaId { get; set; }
        public static int RazredId { get; set; }
        public  static int GodinaId { get; set; }
        public static int MedzlisId { get; set; }
        public static string VjerouciteljId { get; set; }
    }
    public class MuftijaController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext _context;
        public MuftijaController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        
            [HttpGet]
            //Odabir medlisa
            public IActionResult Filtriranje()
            {


                var medzlisi = _context.Medzlisi.ToList();
                ViewData["Medzlisi"] = new MultiSelectList(medzlisi.AsEnumerable(), "MedzlisId", "Naziv");
               
                return View("Filtriranje");
          
            }
            


                


        //Odabir vjeroucitelja
        public async Task<IActionResult> PadajuciIzbornikVjeroucitelj(FilterViewModel medlis)
        {
            if (medlis.MedzlisId ==0)
            {
                return RedirectToAction("Filtriranje");

            }
            else
            {
                ViewBag.Model = new FilterViewModel
                {
                    MedzlisId = medlis.MedzlisId
                };

               
                var adresaId= _context.Medzlisi.Where(m=>m.MedzlisId==medlis.MedzlisId).Select(n=>n.AdresaId).SingleOrDefault();
                var nazivMjesta = _context.Adrese.Where(a => a.AdresaId == adresaId).Select(n => n.NazivMjesta).SingleOrDefault();
                var vjerouciteljiId = _context.Predaje.Select(v=>v.VjerouciteljId).ToList();
                //Pronalazak svih vjerouciteljovih podataka i spremanje u privremenu listu
                var tempVjeroucitelji = new List<AplicationUser>();
                foreach(var vjerouciteljId in vjerouciteljiId)
                {
                    var user = await userManager.FindByIdAsync(vjerouciteljId);
                    if (user.NazivMjesta == nazivMjesta)
                    {
                        tempVjeroucitelji.Add(user);
                        
                    }

                }

                ViewData["Vjeroucitelji"] = new SelectList(tempVjeroucitelji.AsEnumerable(),"Id", "ImeiPrezime");
                
            }
            FilterPodaci.MedzlisId = medlis.MedzlisId;
            return View("Filtriranje");
        }
        //Odabir skola za odabranog vjeroucitelja
        public IActionResult PadajuciIzbornikSkole(FilterViewModel vjeroucitelj)
        {
            if (vjeroucitelj.VjerouciteljId == null)
            {
                return View("Filtriranje");
            }
            else
            {
                var skoleVjeroucitelja = _context.Skole.Where(s => s.VjerouciteljId == vjeroucitelj.VjerouciteljId)
                    .ToList();
                ViewData["Skole"] = new SelectList(skoleVjeroucitelja.AsEnumerable(), "SkolaId", "NazivSkole");
            }
            FilterPodaci.VjerouciteljId = vjeroucitelj.VjerouciteljId;
            return View("Filtriranje");
        }
        //Odabir razreda za odabranu skolu
        public IActionResult PadajuciIzbornikRazred(FilterViewModel skola)
        {
            if (skola.SkolaId == 0)
            {
                return View("Filtriranje");
            }
            else
            {
                var tempRazredi = new List<RazredUcenik>();
                var skole= _context.RazrediUcenik.Where(s => s.SkolaId == skola.SkolaId).ToList();
                foreach(var razred in skole)
                {
                    var element = tempRazredi.Find(x => x.Razred.Contains(razred.Razred));
                    if(element!=null)
                    {
                        continue;
                    }
                    else
                    {
                        tempRazredi.Add(razred);

                    }
                }
                ViewData["Razredi"] = new SelectList(tempRazredi.AsEnumerable(), "RazredUcenikId", "Razred");

            }
            FilterPodaci.SkolaId = skola.SkolaId;
            return View("Filtriranje");
        }
                
         //Padajuci izbornik godina
         public IActionResult PadajuciIzbornikGodina(FilterViewModel odabraniRazred)
        {
            if (odabraniRazred.RazredId ==0) { return View("Filtriranje"); }
            else 
            {
                
                var skolskeGodine = _context.SkolskeGodine.ToList();

               
                
                ViewData["SkolskeGodine"] = new SelectList(skolskeGodine.AsEnumerable(), "SkolskaGodinaId", "Godina");
            }
            FilterPodaci.RazredId=odabraniRazred.RazredId;
            return View("Filtriranje");
        }
        public async Task<IActionResult> FiltriraniUcenici(FilterViewModel model)
            {
            model.MedzlisId = FilterPodaci.MedzlisId;
            model.VjerouciteljId = FilterPodaci.VjerouciteljId;
            model.SkolaId = FilterPodaci.SkolaId;
            model.RazredId = FilterPodaci.RazredId;
            if (model == null)
            {
                RedirectToAction("Filtriranje");
            }
            else
            {
                var users = _context.RazrediUcenik.Where(r => r.SkolskaGodinaId == model.GodinaId && r.MedzlisId==model.MedzlisId && r.SkolaId==model.SkolaId && r.VjerouciteljId==model.VjerouciteljId)
                    .Select(u => u.UcenikId).ToList();
                
                ViewBag.BrojUcenika = users.Count();
                var tempUcenikProfilFlag = new List<UcenikProfilFlag>();
                string tempNazivLokacije = "";
                string tempRazred = "";
                DateTime datumUpisa = DateTime.MinValue;
                foreach (var id in users)
                {
                    var user = await userManager.FindByIdAsync(id);
                    //SkoleUcenika
                    var skoleUcenika = _context.SkoleUcenika.Where(s => s.UcenikId == user.Id).ToList();

                    //Razred ucenika
                    var razrediUcenika = _context.RazrediUcenik.Where(u => u.UcenikId == user.Id && u.DatumIspisa == DateTime.MinValue && model.VjerouciteljId == u.VjerouciteljId).ToList();


                    //Skole i razreedi
                    var skoleRazredi = razrediUcenika.Join(skoleUcenika,
                                                           r => r.UcenikId,
                                                           s => s.UcenikId,
                                                           (razredi, skole) => new {
                                                               Razred = razredi.Razred,
                                                               Skole = skole.NazivSkole
                                                           });

                    foreach (var razredi in skoleRazredi)
                    {
                        tempRazred = razredi.Razred;
                        tempNazivLokacije = razredi.Skole;

                    }







                    //Godina trenutna i datum upisa
                    var result1 = razrediUcenika.Join(_context.SkolskeGodine,
                                                    r => r.SkolskaGodinaId,
                                                    s => s.SkolskaGodinaId,
                                                    (datum_Upisa, godina) => new {
                                                        Datum = datum_Upisa.DatumUpisa,
                                                        Godina = godina.Godina
                                                    });
                    foreach (var godina in result1)
                    {
                        datumUpisa = godina.Datum;
                        ViewBag.Godina = godina.Godina.ToString();
                    }

                    //Naziv medzlisa
                    var result2 = razrediUcenika.Join(_context.Medzlisi,
                                                   r => r.MedzlisId,
                                                   m => m.MedzlisId,

                                                   (naziv, medzlis) => new {
                                                       MedzlisId = naziv.MedzlisId,
                                                       Naziv = medzlis.Naziv

                                                   });
                    foreach (var medzlis in result2)
                    {
                        ViewBag.Medzlis = medzlis.Naziv;
                        ViewBag.Naziv = medzlis.Naziv;
                    }

                    //Provjera dali je popunjen profil dokraja, inicijalizira flag na 0 ili 1
                    if (user.Ulica == null || user.PostanskiBroj == null || user.DatumRodenja == DateTime.MinValue || user.ImeiPrezime == null || user.BrojMobitela == null ||
                        user.ImeiPrezime == null || user.Email == null || user.UserName == null)
                    {
                        int flag = 0;
                        var tempmodel = new UcenikProfilFlag
                        {
                            AplicationUser = user,
                            Flag = flag,
                            Datum = datumUpisa,
                            Razred = tempRazred,
                            LokacijaNastave = tempNazivLokacije

                        };

                        if (tempUcenikProfilFlag.Contains(tempmodel))
                        {
                            continue;
                        }
                        else { tempUcenikProfilFlag.Add(tempmodel); }

                    }
                    else
                    {
                        var flag = 1;
                        var tempmodel = new UcenikProfilFlag
                        {
                            AplicationUser = user,
                            Flag = flag,
                            Datum = datumUpisa,
                            Razred = tempRazred,
                            LokacijaNastave = tempNazivLokacije

                        };
                        if (tempUcenikProfilFlag.Contains(tempmodel))
                        {
                            continue;
                        }
                        else { tempUcenikProfilFlag.Add(tempmodel); }

                    }



                }
                return View(tempUcenikProfilFlag);

            }
            return View();
        }

    }
}
