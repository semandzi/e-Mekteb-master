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
    //[Authorize(Roles = "Admin")]
    public class VjerouciteljController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext _context;
        public VjerouciteljController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager,e_MektebDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context; 
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
                         select u.UcenikId);
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

            var model = new EditUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = (List<string>)userRoles

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
                var result=await userManager.RemoveFromRolesAsync(user,roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Can not remove user exiting roles");
                    return View(model);
                }
                result =await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    ViewBag.Error = $"Can not add selected roles to user";
                }
            }
            return RedirectToAction("EditUser",new { Id = userId });
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
        public async Task<IActionResult> GetStudentsSubjects(string userId)
        {
            var ucenik = await userManager.FindByIdAsync(userId);
            var temp = new List<UcenikAktivnost>();
            if (ucenik == null)
            {
                return NotFound();
            }
            else
            {
                var pohada = _context.Pohada.Where(p => p.UcenikId == ucenik.Id);
                foreach(var predmet in pohada)
                {
                    
                    
                    var ucenikAktivnost = new UcenikAktivnost
                    {
                        NazivPredmeta = pohada.Select(p => p.NazivPredmeta).ToString(),

                    };


                    temp.Add(ucenikAktivnost);

                }

            }


            
            return View();
        }




    }
}
