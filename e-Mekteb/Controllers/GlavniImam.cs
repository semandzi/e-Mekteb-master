using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using e_Mekteb.Models.Administration;
using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers

{
    [Authorize(Roles = "Glavni Imam")]
    public class GlavniImamController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext _context;
        public GlavniImamController(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        [HttpGet]        
        public async Task<IActionResult> ListUsers() {

            var emailOfLoggedUser = HttpContext.User.Identity.Name;
            var user = userManager.FindByEmailAsync(emailOfLoggedUser).Result;                        
            var users = (List<AplicationUser>)await userManager.GetUsersInRoleAsync("Vjeroucitelj");
            users = users.Where(p => p.NazivMjesta == user.NazivMjesta).ToList();
            return View(users);
        }        
    }

}














