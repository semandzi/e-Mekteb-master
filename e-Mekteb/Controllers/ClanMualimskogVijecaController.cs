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
    public class ClanMualimskogVijecaController : Controller
    {
        private readonly e_MektebDbContext _context;

        public ClanMualimskogVijecaController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: ClanMualimskogVijeca
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.ClanoviMualimskogVijeca.Include(c => c.Medzlis);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: ClanMualimskogVijeca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanMualimskogVijeca = await _context.ClanoviMualimskogVijeca
                .Include(c => c.Medzlis)
                .FirstOrDefaultAsync(m => m.ClanMualimskogVijecaId == id);
            if (clanMualimskogVijeca == null)
            {
                return NotFound();
            }

            return View(clanMualimskogVijeca);
        }

        // GET: ClanMualimskogVijeca/Create
        public IActionResult Create()
        {
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "Naziv");
            return View();
        }

        // POST: ClanMualimskogVijeca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClanMualimskogVijecaId,MedzlisId,ImeIPrezimeClanaVijeca,EmailClanaVijeca,KontaktClanaVijeca")] ClanMualimskogVijeca clanMualimskogVijeca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clanMualimskogVijeca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "EmailGlavnogImama", clanMualimskogVijeca.MedzlisId);
            return View(clanMualimskogVijeca);
        }

        // GET: ClanMualimskogVijeca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanMualimskogVijeca = await _context.ClanoviMualimskogVijeca.FindAsync(id);
            if (clanMualimskogVijeca == null)
            {
                return NotFound();
            }
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "Naziv", clanMualimskogVijeca.MedzlisId);
            return View(clanMualimskogVijeca);
        }

        // POST: ClanMualimskogVijeca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClanMualimskogVijecaId,MedzlisId,ImeIPrezimeClanaVijeca,EmailClanaVijeca,KontaktClanaVijeca")] ClanMualimskogVijeca clanMualimskogVijeca)
        {
            if (id != clanMualimskogVijeca.ClanMualimskogVijecaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clanMualimskogVijeca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClanMualimskogVijecaExists(clanMualimskogVijeca.ClanMualimskogVijecaId))
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
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "Naziv", clanMualimskogVijeca.MedzlisId);
            return View(clanMualimskogVijeca);
        }

        // GET: ClanMualimskogVijeca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanMualimskogVijeca = await _context.ClanoviMualimskogVijeca
                .Include(c => c.Medzlis)
                .FirstOrDefaultAsync(m => m.ClanMualimskogVijecaId == id);
            if (clanMualimskogVijeca == null)
            {
                return NotFound();
            }

            return View(clanMualimskogVijeca);
        }

        // POST: ClanMualimskogVijeca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clanMualimskogVijeca = await _context.ClanoviMualimskogVijeca.FindAsync(id);
            _context.ClanoviMualimskogVijeca.Remove(clanMualimskogVijeca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClanMualimskogVijecaExists(int id)
        {
            return _context.ClanoviMualimskogVijeca.Any(e => e.ClanMualimskogVijecaId == id);
        }
    }
}
