using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.Models.Administration;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers

{
    //[Authorize(Roles = "Admin")]
    public class VjerouciteljController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext _context;
        public VjerouciteljController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }


        [HttpPost, ActionName("DeleteUserConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmeed(string id)
        {
            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var vjerouciteljiUcenici = _context.VjerouciteljUcenik.Where(u => u.UcenikId == user.Id).ToList();
                foreach (var vjerouciteljUcenik in vjerouciteljiUcenici)
                {
                    if (vjerouciteljUcenik.VjerouciteljId == vjerouciteljId)
                    {
                        _context.Remove(vjerouciteljUcenik);

                    }
                }
                _context.SaveChanges();

            }

            return RedirectToAction("ListUsers");
        }





        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var User = new List<AplicationUser>();
            User.Add(user);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"There is no user with this {id}";
                return NotFound();

            }

            return View(User);

        }
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId).ToList();
            var ucenici = new AplicationUser();
            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);

            }



            return View(ucenici.Ucenici);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with this {id} can not be found";
                return NotFound();
            }
            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);
            var temp = new List<UcenikAktivnost>();

            var pohada = _context.Pohada.Where(p => p.UcenikId == user.Id).Select(p => p.NazivPredmeta).ToList();
            var skoleUcenika = _context.SkoleUcenika.Where(u => u.UcenikId == id).Select(n => n.NazivSkole).ToList();
            var razrediUcenika = _context.RazrediUcenik.Where(u => u.UcenikId == id && u.DatumIspisa==DateTime.MinValue).Select(n => n.Razred).ToList();

            var model = new EditUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = (List<string>)userRoles,
                PredmetiUcenika = pohada,
                Skole = skoleUcenika,
                RazrediUcenika = razrediUcenika

            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUser model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with this {model.Id} can not be found";
                return NotFound();
            }
            else
            {
                user.Id = model.Id;
                user.UserName = model.UserName;
                user.Email = model.UserName;
                user.AplicationUserId = model.Id;
                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }





        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id{roleId} can not be found";
                return NotFound();
            }
            else
            {
                var model = new List<UserRole>();
                foreach (var user in userManager.Users)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        UserName = user.UserName

                    };

                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRole.IsSelected = true;

                    }
                    else
                    {
                        userRole.IsSelected = false;
                    }
                    model.Add(userRole);
                }
                return View(model);

            }

        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRole> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorDescription = $"Role with id {roleId} can not be found.";
                return NotFound();
            }
            else
            {
                for (int i = 0; i < model.Count(); i++)
                {
                    var user = await userManager.FindByIdAsync(model[i].UserId);
                    IdentityResult result = null;
                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);

                    }

                    else
                    {
                        continue;
                    }

                    if (result.Succeeded)
                    {
                        if (i < (model.Count - 1))
                        {
                            continue;

                        }
                        else
                        {
                            return RedirectToAction("EditRole", new { Id = roleId });
                        }
                    }

                }
                return RedirectToAction("EditRole", new { Id = roleId });

            }
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRoles> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {userId}";
                return NotFound();

            }
            else
            {
                var roles = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Can not remove user exiting roles");
                    return View(model);
                }
                result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    ViewBag.Error = $"Can not add selected roles to user";
                }
            }
            return RedirectToAction("EditUser", new { Id = userId });
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {userId}";
                return NotFound();
            }
            else
            {
                var model = new List<UserRoles>();
                var role = await roleManager.FindByNameAsync("ucenik");
                var userRoles = new UserRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoles.IsSelected = true;

                }
                else
                {
                    userRoles.IsSelected = false;
                }
                model.Add(userRoles);


                return View(model);
            }

        }
        [HttpGet]
        public async Task<IActionResult> DodajPredmetUceniku(string userId)
        {
            ViewBag.userId = userId;
            var ucenik = await userManager.FindByIdAsync(userId);
            var model = new List<AktivnostiUcenika>();

            if (ucenik == null)
            {
                return NotFound();
            }
            else
            {
                var vjeroucitelj = HttpContext.User.Identity.Name;
                var vjerouciteljUserName = await userManager.FindByEmailAsync(vjeroucitelj);
                var vjerouciteljId = vjerouciteljUserName.Id;

                var predmeti = _context.Predaje.Where(p => p.VjerouciteljId == vjerouciteljId).ToList();
                var predmetiUcenikaOvogVjeroucitelja = _context.Pohada.Where(p => p.UcenikId == userId && p.VjerouciteljId == vjerouciteljId).Select(n => n.NazivPredmeta).ToList();
                var predmetiUcenikaDrugihVjeroucitelja = _context.Pohada.Where(p => p.UcenikId == userId && p.VjerouciteljId != vjerouciteljId).Select(n => n.NazivPredmeta).Distinct().ToList();

                foreach (var predmet in predmeti)
                {
                    var aktivnostiUcenika = new AktivnostiUcenika
                    {
                        UcenikId = userId,
                        AktivnostId = predmet.AktivnostId,
                        NazivPredmeta = predmet.NazivPredmeta,
                        VjerouciteljId = predmet.VjerouciteljId
                    };
                    if (predmetiUcenikaOvogVjeroucitelja.Contains(predmet.NazivPredmeta))
                    {
                        aktivnostiUcenika.IsSelected = true;
                        if (predmetiUcenikaDrugihVjeroucitelja.Contains(predmet.NazivPredmeta)) { continue; }
                        else
                        {
                            if (model.Contains(aktivnostiUcenika)) { continue; }
                            else { model.Add(aktivnostiUcenika); }

                        }
                    }
                    else
                    {
                        aktivnostiUcenika.IsSelected = false;
                        if (predmetiUcenikaDrugihVjeroucitelja.Contains(predmet.NazivPredmeta)) { continue; }
                        else { model.Add(aktivnostiUcenika); }

                    }

                }


            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DodajPredmetUceniku(List<AktivnostiUcenika> models, string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"Ne postoji korisnik sa ovim korisničkim imenom{id}";
                return NotFound();
            }
            else
            {
                var vjerouciteljEmail = HttpContext.User.Identity.Name;
                var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljEmail);
                var vjerouciteljId = vjeroucitelj.Id;
                IEnumerable<VjerouciteljAktivnost> predmetiVjeroucitelj = _context.Predaje.Where(v => v.VjerouciteljId == vjerouciteljId).ToList();
                IEnumerable<UcenikAktivnost> predmetiUcenik = _context.Pohada.Where(u => u.UcenikId == user.Id).ToList();
                if (predmetiUcenik.Any())
                {
                    foreach (var model in models)
                    {

                        foreach (var predmetUcenik in predmetiUcenik)
                        {
                            if (model.NazivPredmeta == predmetUcenik.NazivPredmeta)
                            {
                                _context.Remove(predmetUcenik);
                                _context.SaveChanges();
                            }
                        }
                    }
                }



                foreach (var model in models)
                {

                    if (model.IsSelected == true)
                    {
                        var ucenikAktivnosti = new UcenikAktivnost
                        {
                            UcenikId = user.Id,
                            AktivnostId = model.AktivnostId,
                            NazivPredmeta = model.NazivPredmeta,
                            VjerouciteljId = model.VjerouciteljId


                        };
                        _context.Add(ucenikAktivnosti);
                        _context.SaveChanges();
                    }


                }

                return RedirectToAction("EditUser", new { id });


            }
        }
        [HttpGet]
        public IActionResult DodajPostojecegUcenika()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DodajPostojecegUcenika(AplicationUser model)
        {
            if (model.Email == null)
            {
                ModelState.AddModelError("Email", "Email je obavezno polje");
            }
            else
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Niste unijeli ispravan email postojećeg učenika.");


                }
                else
                {
                    var vjerouciteljUserName = HttpContext.User.Identity.Name;
                    var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
                    var vjerouciteljId = vjeroucitelj.Id;
                    var vjerouciteljUcenik = new VjerouciteljUcenik
                    {
                        VjerouciteljId = vjerouciteljId,
                        UcenikId = user.Id,
                        UserName = user.UserName
                    };
                    _context.Add(vjerouciteljUcenik);
                    _context.SaveChanges();
                    return RedirectToAction("ListUsers");

                }
            }



            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> DodajSkoluUceniku(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var ucenikUserName = user.UserName;

            
            
            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            var skole = _context.Skole.Where(s => s.VjerouciteljId == vjerouciteljId).ToList();
            var skoleUcenikaOvogVjeroucitelja = _context.SkoleUcenika
                .Where(s => s.UcenikId == userId && s.VjerouciteljId == vjerouciteljId)
                .Select(s => s.NazivSkole).ToList();

            //Dohvaćanje vjerouciteljId kod kojeg je ucenik trenutno upisan u školu
            var trenutniUpisaniRazredVjerouciteljId = _context.RazrediUcenik.Where(r => r.DatumIspisa == DateTime.MinValue && r.UcenikId == userId)
                .Select(r => r.VjerouciteljId).SingleOrDefault();
            var trenutniUpisaniRazredUser = await userManager.FindByIdAsync(trenutniUpisaniRazredVjerouciteljId);
            var trenutniUpisaniRazredUserName = trenutniUpisaniRazredUser.UserName;
            ViewBag.trenutniUpisaniRazredUserName = trenutniUpisaniRazredUserName;

            ViewBag.userId = userId;
            ViewBag.ucenikUserName = ucenikUserName;
            ViewBag.vjerouciteljUserName = vjerouciteljUserName;

            var skoleUcenikaDrugihVjeroucitelja = _context.SkoleUcenika
                .Where(s => s.UcenikId == userId && s.VjerouciteljId != vjerouciteljId)
                .Select(s => s.NazivSkole).ToList();

            if (skole == null)
            {
                ViewBag.Error = $"Niste unijeli svoje škole.";
                return NotFound();
            }
            else
            {

                var tempLista = new List<SkolaUcenikView>();
                var tempSkoleLista = new SkoleLista();
                foreach (var skola in skole)
                {

                    var skolaUcenik = new SkolaUcenikView
                    {

                        SkolaId = skola.SkolaId,
                        NazivSkole = skola.NazivSkole,
                        VjerouciteljId = vjerouciteljId
                    };


                    if (skoleUcenikaOvogVjeroucitelja.Contains(skola.NazivSkole))
                    {
                        tempSkoleLista.IsSelected = null;

                        if (tempLista.Contains(skolaUcenik)) { continue; }
                        else { tempLista.Add(skolaUcenik); }


                    }
                    else
                    {
                        tempSkoleLista.IsSelected = null;
                        if (skoleUcenikaDrugihVjeroucitelja.Any()) { continue; }
                        else { tempLista.Add(skolaUcenik); }

                    }










                }
                tempSkoleLista.Skole = tempLista;
                return View(tempSkoleLista);
            }


        }
        [HttpPost]
        public async Task<IActionResult> DodajSkoluUceniku(SkoleLista models, string id)
        {

            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            var skoleUcenika = _context.SkoleUcenika.Where(s => s.UcenikId == id).ToList();
            var skole = _context.Skole.Where(s => s.SkolaId.ToString() == models.IsSelected).ToList();
            var skoleOvogVjeroucitelja = _context.Skole.Where(s => s.VjerouciteljId == vjerouciteljId).ToList();
            if (!skoleOvogVjeroucitelja.Any())
            {
                return RedirectToAction("Create", "Skola");
            }





            else
            {
                foreach (var skolaUcenik in skoleUcenika)
                {
                    _context.Remove(skolaUcenik);

                }


                if (models.IsSelected != null)
                {
                    var tempSkola = new SkolaUcenik
                    {

                        NazivSkole = skole.Select(n => n.NazivSkole).SingleOrDefault(),
                        VjerouciteljId = vjerouciteljId,
                        UcenikId = id,
                        SkolaId = skole.Select(n => n.SkolaId).SingleOrDefault()
                    };

                    _context.Add(tempSkola);
                }


                _context.SaveChanges();









                return RedirectToAction("EditUser", new { id });
            }



        }

        [HttpGet]
        public async Task<IActionResult> DodajRazredUceniku(string userId)
        {
            ViewBag.userId = userId;

            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            var razredi = _context.Razredi.ToList();
            var razrediUcenikaOvogVjeroucitelja = _context.RazrediUcenik
                .Where(s => s.UcenikId == userId && s.VjerouciteljId == vjerouciteljId)
                .Select(s => s.Razred).ToList();

            //Dohvaćanje vjerouciteljId kod kojeg je ucenik trenutno upisan u školu
            var trenutniUpisaniRazredVjerouciteljId = _context.RazrediUcenik.Where(r => r.DatumIspisa == DateTime.MinValue && r.UcenikId == userId)
                .Select(r => r.VjerouciteljId).SingleOrDefault();
            var trenutniUpisaniRazredUser = await userManager.FindByIdAsync(trenutniUpisaniRazredVjerouciteljId);
            var trenutniUpisaniRazredUserName = trenutniUpisaniRazredUser.UserName;
            ViewBag.trenutniUpisaniRazredUserName = trenutniUpisaniRazredUserName;
            var razrediUcenikaDrugihVjeroucitelja = _context.RazrediUcenik
                .Where(s => s.UcenikId == userId && s.VjerouciteljId != vjerouciteljId)
                .Select(s => s.Razred).ToList();
            var skolskeGodine = _context.SkolskeGodine;

            var razrediUcenika = _context.RazrediUcenik.Where(s => s.UcenikId == userId && s.DatumIspisa==DateTime.MinValue).ToList();
            var exist=razrediUcenika.Any();
            if (exist)
            {
                ViewBag.exist = 1;
            }
            else { ViewBag.exist = 0; }

            
            ViewData["SkolskaGodinaId"] = new SelectList(skolskeGodine.AsEnumerable(), "SkolskaGodinaId", "Godina");
            ViewBag.skolskeGodine = skolskeGodine;
            if (razredi == null)
            {
                ViewBag.Error = $"Niste unijeli svoje škole.";
                return NotFound();
            }
            else
            {

                var tempLista = new List<RazredUcenikView>();
                var tempSkoleLista = new RazrediUcenikView();
                foreach (var razred in razredi)
                {

                    var razredUcenikView = new RazredUcenikView
                    {

                        RazredId = razred.RazredId.ToString(),
                        Razred = razred.Naziv,

                    };


                    if (razrediUcenikaOvogVjeroucitelja.Contains(razred.Naziv))
                    {
                        tempSkoleLista.IsSelected = null;

                        if (tempLista.Contains(razredUcenikView)) { continue; }
                        else { tempLista.Add(razredUcenikView); }


                    }
                    else
                    {
                        tempSkoleLista.IsSelected = null;
                        if (razrediUcenikaDrugihVjeroucitelja.Any()) { continue; }
                        else { tempLista.Add(razredUcenikView); }

                    }










                }
                tempSkoleLista.Razredi = tempLista;
                return View(tempSkoleLista);
            }


        }

        [HttpPost]
        public async Task<IActionResult> DodajRazredUceniku(RazrediUcenikView models, string id,string ispis)
        {
           

            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            var razrediUcenika = _context.RazrediUcenik.Where(s => s.UcenikId == id).ToList();
            var razredi = _context.Razredi.ToList();
            var razrediOvogVjeroucitelja = _context.RazrediUcenik.Where(s => s.VjerouciteljId == vjerouciteljId && s.UcenikId==id).ToList();

            
            if (id == null || models.IsSelected==null || models.SkolskaGodinaId==0)
            {
                
                    return RedirectToAction("EditUser", new { id });
                
            
            }





            else
            {
               

                var odabraniRazredNaziv = razredi.Where(r => r.RazredId.ToString() == models.IsSelected)
                    .Select(r => r.Naziv).SingleOrDefault();
                var skolaId = _context.SkoleUcenika.Where(m => m.UcenikId==id)
                    .Select(s=>s.SkolaId).SingleOrDefault();
                if (skolaId == 0) {return RedirectToAction("EditUser",new { id}); }

                var medzlisId = _context.Skole.Where(m => m.SkolaId==skolaId)
                    .Select(s => s.MedzlisId).SingleOrDefault();
                var updateRazredUcenik = new RazredUcenik();
                if (ispis == "true") 
                {
                    updateRazredUcenik.DatumIspisa = DateTime.Now;
                    _context.Update(updateRazredUcenik);
                }

                if (models.IsSelected != null)
                {
                    //ispis iz razreda
                    var razredUcenik = _context.RazrediUcenik.Where(u => u.UcenikId == id && u.DatumIspisa == DateTime.MinValue).SingleOrDefault();
                    if (razredUcenik != null)
                    {
                        razredUcenik.DatumIspisa = DateTime.Now;
                        _context.Update(razredUcenik);
                        _context.SaveChanges();
                    }
                    
                    
                        //upis u novi razred
                        var tempRazredUcenik = new RazredUcenik
                        {

                            Razred = odabraniRazredNaziv,
                            VjerouciteljId = vjerouciteljId,
                            UcenikId = id,
                            SkolaId = skolaId,
                            SkolskaGodinaId = models.SkolskaGodinaId,
                            MedzlisId = medzlisId
                        };

                        tempRazredUcenik.DatumUpisa = DateTime.Now;
                        tempRazredUcenik.DatumIspisa = DateTime.MinValue;
                        _context.Add(tempRazredUcenik);

                        _context.SaveChanges();
                    

                   

                   

                    
                }
               









                return RedirectToAction("EditUser", new { id });
            }



        }
       
        public IActionResult IspisUcenika(string id)
        {
            if (id == null)
            {
                ViewBag.Error = $"Ucenik sa ovim {id} nema upisanih razreda";
                return NotFound();
            }
            var razredUcenik = _context.RazrediUcenik.Where(u => u.UcenikId == id && u.DatumIspisa==DateTime.MinValue).SingleOrDefault();
            return View(razredUcenik);
        }

        [HttpPost]
        public IActionResult IspisUcenikaPotvrda(string id)
        {
            
            
            var razredUcenik = _context.RazrediUcenik.Where(u => u.UcenikId == id && u.DatumIspisa == DateTime.MinValue).SingleOrDefault();
            if (razredUcenik != null)
            {
                razredUcenik.DatumIspisa = DateTime.Now;
                _context.Update(razredUcenik);
                _context.SaveChanges();
                return RedirectToAction("EditUser", new { id });
            }
            return RedirectToAction("EditUser", new { id });

        }































    }














}
