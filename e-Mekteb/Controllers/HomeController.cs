using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using e_Mekteb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using e_Mekteb.ApDbContext;
using e_Mekteb.ViewModel;

namespace e_Mekteb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AplicationUser> userManager;
        private readonly e_MektebDbContext context;

        public HomeController(ILogger<HomeController> logger, UserManager<AplicationUser> userManager, e_MektebDbContext context)
        {
            _logger = logger;
            this.userManager = userManager;
            this.context = context;
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {


            var username = HttpContext.User.Identity.Name;
            var ucenik = await userManager.FindByNameAsync(username);
            var ucenikId = ucenik.Id;
            var tempObavijesti = new List<Obavijest>();
            var tempVjeroucitelji = new List<AplicationUser>();

            var vjerouciteljucenik = context.VjerouciteljUcenik.Where(p => p.UcenikId == ucenikId);
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
    }
}
