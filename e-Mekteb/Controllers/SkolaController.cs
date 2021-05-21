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

namespace e_Mekteb.Controllers
{
    public class SkolaController : Controller
    {
        private readonly e_MektebDbContext _context;
        private readonly UserManager<AplicationUser> userManager;

        public SkolaController(e_MektebDbContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
          this.userManager = userManager;
        }

        // GET: Skola
        public async Task<IActionResult> Index()
        {
            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByEmailAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            return View(await _context.Skole.Where(v=>v.VjerouciteljId==vjerouciteljId).ToListAsync());
        }

        // GET: Skola/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skola = await _context.Skole
                .FirstOrDefaultAsync(m => m.SkolaId == id);
            if (skola == null)
            {
                return NotFound();
            }

            return View(skola);
        }

        // GET: Skola/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skola/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkolaId,NazivSkole,Grad,Adresa,PostanskiBroj")] Skola skola)
        {
            var vjerouciteljUserName = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(vjerouciteljUserName);
            var vjerouciteljId = vjeroucitelj.Id;
            var skole = _context.Skole.Where(s => s.VjerouciteljId == vjerouciteljId).ToList();

            if (ModelState.IsValid)
            {
                skola.VjerouciteljId = vjerouciteljId;
                _context.Add(skola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skola);
        }

        // GET: Skola/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skola = await _context.Skole.FindAsync(id);
            if (skola == null)
            {
                return NotFound();
            }
            return View(skola);
        }

        // POST: Skola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkolaId,NazivSkole,Grad,Adresa,PostanskiBroj")] Skola skola)
        {
            if (id != skola.SkolaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkolaExists(skola.SkolaId))
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
            return View(skola);
        }

        // GET: Skola/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skola = await _context.Skole
                .FirstOrDefaultAsync(m => m.SkolaId == id);
            if (skola == null)
            {
                return NotFound();
            }

            return View(skola);
        }

        // POST: Skola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skola = await _context.Skole.FindAsync(id);
            _context.Skole.Remove(skola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkolaExists(int id)
        {
            return _context.Skole.Any(e => e.SkolaId == id);
        }
    }
}
