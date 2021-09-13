using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Controllers
{
    public class GlavniImam : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
