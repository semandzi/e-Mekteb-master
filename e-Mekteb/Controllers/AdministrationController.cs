using e_Mekteb.Models;
using e_Mekteb.Models.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers

{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AplicationUser> userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
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
                foreach(var role in roleManager.Roles)
                {
                    var userRoles = new UserRoles
                    {
                        RoleId=role.Id,
                        RoleName=role.Name
                    };
                    if(await userManager.IsInRoleAsync(user, role.Name))
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
            if (result.Succeeded)
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
            var User=new List<AplicationUser>();
            User.Add(user);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"There is no user with this {id}";
                return NotFound();

            }

            return View(User);

        }
        [HttpGet]
        public IActionResult ListUsers()
        {
           var users= userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user== null)
            {
                ViewBag.ErrorMessage = $"User with this {id} can not be found";
                return NotFound();
            }
            var userRoles =await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);

            var model = new EditUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email=user.Email,
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
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach(var error in result.Errors)
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
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRole()
        {
            var roles=roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {

            var role = await roleManager.FindByIdAsync(id);
            if(role==null)
            {
                ViewBag.ErrorMessage = $"Role with id{id} can not be found";
                return NotFound();
            }
            var model = new EditRole()
            {
                Id = role.Id,
               RoleName = role.Name
            };
            foreach(var user in userManager.Users )
            {
                if(await userManager.IsInRoleAsync(user,role.Name))
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
               var result=await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            
        }
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
                foreach(var user in userManager.Users)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        UserName = user.UserName

                    };

                    if(await userManager.IsInRoleAsync(user,role.Name))
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
            if(role==null)
            {
                ViewBag.ErrorDescription = $"Role with id {roleId} can not be found.";
                return NotFound();
            }
            else
            {
                for (int i= 0;i< model.Count(); i++)
                {
                    var user=await userManager.FindByIdAsync(model[i].UserId);
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

                    if(result.Succeeded)
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

       
    }
}
