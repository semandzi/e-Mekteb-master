using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.Models.Administration;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers

{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext _context;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {userId}";
                return NotFound();

            }
            else
            {
                var existingUserClaims = await userManager.GetClaimsAsync(user);
                var model = new UserClaims
                {
                    UserId = userId

                };
                foreach (Claim claim in ClaimsStore.AllClaims)
                {
                    UserClaim userClaim = new UserClaim
                    {
                        ClaimType = claim.Type
                    };
                    if (existingUserClaims.Any(c => c.Type == claim.Type))
                    {
                        userClaim.IsSlected = true;
                    }
                    model.Claims.Add(userClaim);
                }
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaims model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {model.UserId}";
                return NotFound();

            }
            else
            {
                var claims = await userManager.GetClaimsAsync(user);
                var result = await userManager.RemoveClaimsAsync(user, claims);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Can not remove user existing claims");
                    return View(model);
                }

                result = await userManager.AddClaimsAsync(user, model.Claims.Where(c => c.IsSlected).Select(c => new Claim(c.ClaimType, c.ClaimType)));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Can not add selected claims to the user");
                    return View(model);
                }

                return RedirectToAction("EditUser", new { Id = model.UserId });
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
                foreach (var role in roleManager.Roles.ToList())
                {
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

                }
                return View(model);
            }

        }
        [HttpPost, ActionName("DeleteUserConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmeed(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("ListUsers");

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
            var temp= new List<AplicationUser>();
            foreach (var user in userManager.Users.ToList())
            {
                
                if (await userManager.IsInRoleAsync(user, "Vjeroucitelj"))
                {
                    
                    temp.Add(user);
                    
                }

            }
            
            return View(temp);

            //var users = userManager.Users.Select();
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
            var predmetiVjeroucitelja = _context.Predaje.Where(v => v.VjerouciteljId == id).Select(v => v.NazivPredmeta).ToList();


            var model = new EditUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = (List<string>)userRoles,
                Predmeti=predmetiVjeroucitelja

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
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Name
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id{id} can not be found";
                return NotFound();
            }
            var model = new EditRole()
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRole model)
        {

            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id{model.Id} can not be found";
                return NotFound();
            }
            else
            {
                role.Id = model.Id;
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }


        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.Error = $"There is no role with that {id}";
                return NotFound();

            }
            else
            {
                var Role = new List<IdentityRole>();
                Role.Add(role);
                return View(Role);
            }

        }

        [Authorize(Policy = "DeleteRolePolicy")]
        [HttpPost, ActionName("DeleteRoleConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoleConfirmed(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return RedirectToAction("ListRole");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("ListRole");

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
                foreach (var user in userManager.Users.ToList())
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

        [HttpGet]
        public async Task<IActionResult> PredmetiVjeroucitelja(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Error = $"There is no user with this {userId}";
                return NotFound();
            }
            else
            {

                var aktivnost = _context.Aktivnosti.ToList(); 
                var predaje = _context.Predaje.Where(v => v.VjerouciteljId==user.Id).ToList();
                var model = new List<AktivnostiVjeroucitelja>();
                ViewBag.userId = user.Id;
                foreach (var predmet in aktivnost)
                {


                    var aktivnostVjeroucitelja = new AktivnostiVjeroucitelja
                    {
                        VjerouciteljId = user.Id,
                        AktivnostId = predmet.AktivnostId,
                        NazivPredmeta = predmet.Naziv

                    };
                    foreach(var predmetVjeroucitelja in predaje)
                    {
                        if (predmetVjeroucitelja.NazivPredmeta == predmet.Naziv)
                        {
                            aktivnostVjeroucitelja.IsSelected = true;

                        }
                        else
                        {
                            continue;
                        }


                    }

                   
                    
                    model.Add(aktivnostVjeroucitelja);

                }
                return View(model);

            }
                        
        }
                        
        [HttpPost]
        public async Task<IActionResult> PredmetiVjeroucitelja(List<AktivnostiVjeroucitelja> list,string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (userId == null)
            {
                ViewBag.Error = $"Ne postoji korisnik sa ovim Id {userId}";
                return NotFound();
            }
            else
            {
                string vjerouciteljId = userId;
                var listofPredaje = _context.Predaje.Where(m => m.VjerouciteljId == vjerouciteljId).ToList();
                var listofPohada = _context.Pohada.ToList();
                foreach(var predmetVjeroucitelj in listofPredaje)
                {
                    foreach(var predmetUcenik in listofPohada)
                    {
                        if(predmetVjeroucitelj.NazivPredmeta==predmetUcenik.NazivPredmeta && vjerouciteljId == predmetVjeroucitelj.VjerouciteljId)
                        {
                            _context.Remove(predmetUcenik);
                        }
                    }

                }
                _context.RemoveRange(listofPredaje);
                _context.SaveChanges();
                foreach (var model in list)
                {




                    if (model.IsSelected == true)
                    {
                        var aktivnost = _context.Aktivnosti.ToList();

                        var vjeroucitelj = new AktivnostiVjeroucitelja
                        {
                            AktivnostId = model.AktivnostId,
                            VjerouciteljId = model.VjerouciteljId,
                            IsSelected = model.IsSelected,
                            NazivPredmeta = aktivnost.Where(a => a.AktivnostId == model.AktivnostId).Select(a => a.Naziv).FirstOrDefault()
                        };
                        var vjerouciteljAktivnost = new VjerouciteljAktivnost
                        {
                            AktivnostId = vjeroucitelj.AktivnostId,
                            VjerouciteljId = vjeroucitelj.VjerouciteljId,
                            NazivPredmeta = vjeroucitelj.NazivPredmeta

                        };
                        
                        _context.Add(vjerouciteljAktivnost);
                        await _context.SaveChangesAsync();

                    }
                }
                
                return RedirectToAction("EditUser",new { Id=userId});

            }


        }

    }

}














