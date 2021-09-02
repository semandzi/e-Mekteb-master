using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using Microsoft.AspNetCore.Identity;
using e_Mekteb.ViewModel;

namespace e_Mekteb.Controllers
{
    public class ObavijestController : Controller
    {
        private readonly e_MektebDbContext _context;
        private readonly UserManager<AplicationUser> userManager;

        public ObavijestController(e_MektebDbContext context, UserManager<AplicationUser> userManager)
        {
            this.userManager = userManager;
            _context = context;
        }

        //GET: Obavijest
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            
            var tempObavijesti = new List<Obavijest>();
            
                var user = await userManager.FindByIdAsync(vjerouciteljId);
                var obavijesti = _context.Obavijesti.Where(a => a.VjerouciteljId == vjerouciteljId).ToList();
                foreach (var obavijest in obavijesti)
                {
                    tempObavijesti.Add(obavijest);
                }

            

           

            var model = new Obavijesti
            {
                obavijesti = tempObavijesti,
              

            };

            return View(model);

        }

        // GET: Obavijest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijesti
                .FirstOrDefaultAsync(m => m.ObavijestId == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            return View(obavijest);
        }

        // GET: Obavijest/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Obavijest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObavijestId,Naslov,Sadrzaj")] Obavijest obavijest)
        {
            if (ModelState.IsValid)
            {
                var name = HttpContext.User.Identity.Name;
                var vjeroucitelj = await userManager.FindByNameAsync(name);
                var vjerouciteljId = vjeroucitelj.Id;
                obavijest.VjerouciteljId = vjerouciteljId;
                _context.Add(obavijest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obavijest);
        }

        // GET: Obavijest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijesti.FindAsync(id);
            if (obavijest == null)
            {
                return NotFound();
            }
            return View(obavijest);
        }

        // POST: Obavijest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObavijestId,Naslov,Sadrzaj")] Obavijest obavijest)
        {
            if (id != obavijest.ObavijestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var name = HttpContext.User.Identity.Name;
                    var vjeroucitelj = await userManager.FindByNameAsync(name);
                    var vjerouciteljId = vjeroucitelj.Id;
                    obavijest.VjerouciteljId = vjerouciteljId;
                    _context.Update(obavijest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObavijestExists(obavijest.ObavijestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obavijest);
        }

        // GET: Obavijest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijesti
                .FirstOrDefaultAsync(m => m.ObavijestId == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            return View(obavijest);
        }

        // POST: Obavijest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obavijest = await _context.Obavijesti.FindAsync(id);
            _context.Obavijesti.Remove(obavijest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObavijestExists(int id)
        {
            return _context.Obavijesti.Any(e => e.ObavijestId == id);
        }
    }
}
