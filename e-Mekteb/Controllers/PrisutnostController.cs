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
    public class PrisutnostController : Controller
    {
        private readonly e_MektebDbContext _context;

        public PrisutnostController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: Prisutnost
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.Prisutnosti.Include(p => p.Aktivnost).Include(p=>p.AplicationUser);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: Prisutnost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisutnost = await _context.Prisutnosti
                .Include(p => p.Aktivnost).Include(p=>p.AplicationUser)
                .FirstOrDefaultAsync(m => m.PrisutnostId == id);
            if (prisutnost == null)
            {
                return NotFound();
            }

            return View(prisutnost);
        }

        // GET: Prisutnost/Create
        public IActionResult Create()
        {
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv");
            ViewData["AplicationUserId"] = new SelectList(_context.Users, "AplicationUserId", "Email");
            var enumPrisutnost = Enum.GetValues(typeof(IsPrisutan)).Cast<IsPrisutan>().Select(v => v.ToString()).ToList();
            ViewData["Prisutnost"] = new SelectList(enumPrisutnost);

            return View();
        }

        // POST: Prisutnost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrisutnostId,Datum,AplicationUserId,AktivnostId,IsPrisutan")] Prisutnost prisutnost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prisutnost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", prisutnost.AktivnostId);
            ViewData["AplicationUserId"] = new SelectList(_context.Users, "AplicationUserId", "Email", prisutnost.AplicationUserId);
            var enumPrisutnost = Enum.GetValues(typeof(IsPrisutan)).Cast<IsPrisutan>().Select(v => v.ToString()).ToList();
            ViewData["Prisutnost"] = new SelectList(enumPrisutnost);
            return View(prisutnost);
        }

        // GET: Prisutnost/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisutnost = await _context.Prisutnosti.FindAsync(id);
            if (prisutnost == null)
            {
                return NotFound();
            }
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", prisutnost.AktivnostId);
            ViewData["AplicationUserId"] = new SelectList(_context.Users, "AplicationUserId", "Email",prisutnost.AplicationUserId);
            var enumPrisutnost = Enum.GetValues(typeof(IsPrisutan)).Cast<IsPrisutan>().Select(v => v.ToString()).ToList();
            ViewData["Prisutnost"] = new SelectList(enumPrisutnost);
            return View(prisutnost);
        }

        // POST: Prisutnost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrisutnostId,Datum,AplicationUserId,AktivnostId,IsPrisutan")] Prisutnost prisutnost)
        {
            if (id != prisutnost.PrisutnostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prisutnost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrisutnostExists(prisutnost.PrisutnostId))
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
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", prisutnost.AktivnostId);
            return View(prisutnost);
        }

        // GET: Prisutnost/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisutnost = await _context.Prisutnosti
                .Include(p => p.Aktivnost)
                .FirstOrDefaultAsync(m => m.PrisutnostId == id);
            if (prisutnost == null)
            {
                return NotFound();
            }

            return View(prisutnost);
        }

        // POST: Prisutnost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prisutnost = await _context.Prisutnosti.FindAsync(id);
            _context.Prisutnosti.Remove(prisutnost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrisutnostExists(int id)
        {
            return _context.Prisutnosti.Any(e => e.PrisutnostId == id);
        }
    }
}
