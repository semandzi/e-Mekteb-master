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
    public class SkolskaGodinaController : Controller
    {
        private readonly e_MektebDbContext _context;

        public SkolskaGodinaController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: SkolskaGodina
        public async Task<IActionResult> Index()
        {
            return View(await _context.SkolskeGodine.ToListAsync());
        }

        // GET: SkolskaGodina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skolskaGodina = await _context.SkolskeGodine
                .FirstOrDefaultAsync(m => m.SkolskaGodinaId == id);
            if (skolskaGodina == null)
            {
                return NotFound();
            }

            return View(skolskaGodina);
        }

        // GET: SkolskaGodina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SkolskaGodina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkolskaGodinaId,Godina")] SkolskaGodina skolskaGodina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skolskaGodina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skolskaGodina);
        }

        // GET: SkolskaGodina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skolskaGodina = await _context.SkolskeGodine.FindAsync(id);
            if (skolskaGodina == null)
            {
                return NotFound();
            }
            return View(skolskaGodina);
        }

        // POST: SkolskaGodina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkolskaGodinaId,Godina")] SkolskaGodina skolskaGodina)
        {
            if (id != skolskaGodina.SkolskaGodinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skolskaGodina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkolskaGodinaExists(skolskaGodina.SkolskaGodinaId))
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
            return View(skolskaGodina);
        }

        // GET: SkolskaGodina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skolskaGodina = await _context.SkolskeGodine
                .FirstOrDefaultAsync(m => m.SkolskaGodinaId == id);
            if (skolskaGodina == null)
            {
                return NotFound();
            }

            return View(skolskaGodina);
        }

        // POST: SkolskaGodina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skolskaGodina = await _context.SkolskeGodine.FindAsync(id);
            _context.SkolskeGodine.Remove(skolskaGodina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkolskaGodinaExists(int id)
        {
            return _context.SkolskeGodine.Any(e => e.SkolskaGodinaId == id);
        }
    }
}
