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
    public class AdresaController : Controller
    {
        private readonly e_MektebDbContext _context;

        public AdresaController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: Adresa
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adrese.ToListAsync());
        }

        // GET: Adresa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresa = await _context.Adrese
                .FirstOrDefaultAsync(m => m.AdresaId == id);
            if (adresa == null)
            {
                return NotFound();
            }

            return View(adresa);
        }

        // GET: Adresa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adresa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdresaId,NazivMjesta,Ulica,PostanskiBroj")] Adresa adresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adresa);
        }

        // GET: Adresa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresa = await _context.Adrese.FindAsync(id);
            if (adresa == null)
            {
                return NotFound();
            }
            return View(adresa);
        }

        // POST: Adresa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdresaId,NazivMjesta,Ulica,PostanskiBroj")] Adresa adresa)
        {
            if (id != adresa.AdresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresaExists(adresa.AdresaId))
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
            return View(adresa);
        }

        // GET: Adresa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresa = await _context.Adrese
                .FirstOrDefaultAsync(m => m.AdresaId == id);
            if (adresa == null)
            {
                return NotFound();
            }

            return View(adresa);
        }

        // POST: Adresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adresa = await _context.Adrese.FindAsync(id);
            _context.Adrese.Remove(adresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdresaExists(int id)
        {
            return _context.Adrese.Any(e => e.AdresaId == id);
        }
    }
}
