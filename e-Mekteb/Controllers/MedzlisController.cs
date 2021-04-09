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
    public class MedzlisController : Controller
    {
        private readonly e_MektebDbContext _context;

        public MedzlisController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: Medzlis
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.Medzlisi.Include(m => m.Adresa);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: Medzlis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medzlis = await _context.Medzlisi
                .Include(m => m.Adresa)
                .FirstOrDefaultAsync(m => m.MedzlisId == id);
            if (medzlis == null)
            {
                return NotFound();
            }

            return View(medzlis);
        }

        // GET: Medzlis/Create
        public IActionResult Create()
        {
            ViewData["AdresaId"] = new SelectList(_context.Adrese, "AdresaId", "NazivMjesta");
            return View();
        }

        // POST: Medzlis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedzlisId,AdresaId,Naziv,Kontakt,GlavniImam,EmailGlavnogImama")] Medzlis medzlis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medzlis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresaId"] = new SelectList(_context.Adrese, "AdresaId", "NazivMjesta", medzlis.AdresaId);
            return View(medzlis);
        }

        // GET: Medzlis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medzlis = await _context.Medzlisi.FindAsync(id);
            if (medzlis == null)
            {
                return NotFound();
            }
            ViewData["AdresaId"] = new SelectList(_context.Adrese, "AdresaId", "NazivMjesta", medzlis.AdresaId);
            return View(medzlis);
        }

        // POST: Medzlis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedzlisId,AdresaId,Naziv,Kontakt,GlavniImam,EmailGlavnogImama")] Medzlis medzlis)
        {
            if (id != medzlis.MedzlisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medzlis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedzlisExists(medzlis.MedzlisId))
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
            ViewData["AdresaId"] = new SelectList(_context.Adrese, "AdresaId", "NazivMjesta", medzlis.AdresaId);
            return View(medzlis);
        }

        // GET: Medzlis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medzlis = await _context.Medzlisi
                .Include(m => m.Adresa)
                .FirstOrDefaultAsync(m => m.MedzlisId == id);
            if (medzlis == null)
            {
                return NotFound();
            }

            return View(medzlis);
        }

        // POST: Medzlis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medzlis = await _context.Medzlisi.FindAsync(id);
            _context.Medzlisi.Remove(medzlis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedzlisExists(int id)
        {
            return _context.Medzlisi.Any(e => e.MedzlisId == id);
        }
    }
}
