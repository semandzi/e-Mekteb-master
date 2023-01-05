using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System;

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
            
            var teachers = await GetMyTeachers();
            
            return View( await GetMyTeachers());
        }
        private async Task<List<AplicationUser>> GetMyTeachers() {
            var emailOfLoggedUser = HttpContext.User.Identity.Name;
            var user = userManager.FindByEmailAsync(emailOfLoggedUser).Result;
            var users = (List<AplicationUser>)await userManager.GetUsersInRoleAsync("Vjeroucitelj");
            users = users.Where(p => p.NazivMjesta == user.NazivMjesta).OrderBy(n => n.ImeiPrezime).ToList();
            return users;
        }

        [HttpGet]
        public async Task<ContentResult> SortAscending()
        {
            var users = await GetMyTeachers();
            users = users.OrderByDescending(n => n.ImeiPrezime).ToList();
            
            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                };
                var json = JsonConvert.SerializeObject(users, jsonSerializerSettings);
                return Content(json.ToString(), "application/json"); ;
            }
            catch(Exception ex) { throw ex; }
            
        }
        public async Task<IActionResult> SortDescending()
        {
            var users = await GetMyTeachers();
            users = users.OrderByDescending(n => n.ImeiPrezime).ToList();
            return View(users);
        }
    }

}














