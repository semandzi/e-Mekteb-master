using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_Mekteb.ApDbContext;
using e_Mekteb.Models;

namespace e_Mekteb.Controllers
{
    public class SkolaController : Controller
    {
        private readonly e_MektebDbContext _context;

        public SkolaController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: Skola
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.Skole.Include(s => s.VjerouciteljViewModel);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: Skola/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skola = await _context.Skole
                .Include(s => s.VjerouciteljViewModel)
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
            ViewData["VjerouciteljViewModelId"] = new SelectList(_context.Vjeroucitelji, "VjerouciteljViewModelId", "Email");
            return View();
        }

        // POST: Skola/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkolaId,VjerouciteljViewModelId,NazivSkole")] Skola skola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VjerouciteljViewModelId"] = new SelectList(_context.Vjeroucitelji, "VjerouciteljViewModelId", "Email", skola.VjerouciteljViewModelId);
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
            ViewData["VjerouciteljViewModelId"] = new SelectList(_context.Vjeroucitelji, "VjerouciteljViewModelId", "Email", skola.VjerouciteljViewModelId);
            return View(skola);
        }

        // POST: Skola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkolaId,VjerouciteljViewModelId,NazivSkole")] Skola skola)
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
            ViewData["VjerouciteljViewModelId"] = new SelectList(_context.Vjeroucitelji, "VjerouciteljViewModelId", "Email", skola.VjerouciteljViewModelId);
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
                .Include(s => s.VjerouciteljViewModel)
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
