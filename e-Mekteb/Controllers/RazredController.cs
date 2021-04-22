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
    public class RazredController : Controller
    {
        private readonly e_MektebDbContext _context;
        private readonly UserManager<AplicationUser> userManager;

        public RazredController(e_MektebDbContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Razred
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.Razredi.Include(r => r.Medzlis).Include(r => r.SkolskaGodina);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: Razred/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razred = await _context.Razredi
                .Include(r => r.Medzlis)
                .Include(r => r.SkolskaGodina)
                .FirstOrDefaultAsync(m => m.RazredId == id);
            if (razred == null)
            {
                return NotFound();
            }

            return View(razred);
        }

        // GET: Razred/Create
        public async Task<IActionResult> Create()
        {
            var temp = new List<AplicationUser>();
            foreach (var user in userManager.Users)
            {

                if (await userManager.IsInRoleAsync(user, "Vjeroucitelj"))
                {

                    temp.Add(user);

                }

            }
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "Naziv");
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina");
            ViewData["VjerouciteljId"] = new SelectList(temp,"AplicationUserId","Email");
            return View();
        }

        // POST: Razred/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RazredId,SkolskaGodinaId,MedzlisId,AplicationUserId,Naziv")] Razred razred)
        {
            if (ModelState.IsValid)
            {
                _context.Add(razred);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "Naziv", razred.MedzlisId);
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina", razred.SkolskaGodinaId);
            return View(razred);
        }

        // GET: Razred/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razred = await _context.Razredi.FindAsync(id);
            if (razred == null)
            {
                return NotFound();
            }
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "Naziv", razred.MedzlisId);
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina", razred.SkolskaGodinaId);
            return View(razred);
        }

        // POST: Razred/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RazredId,SkolskaGodinaId,MedzlisId,AplicationUserId,Naziv")] Razred razred)
        {
            if (id != razred.RazredId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(razred);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazredExists(razred.RazredId))
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
            ViewData["MedzlisId"] = new SelectList(_context.Medzlisi, "MedzlisId", "EmailGlavnogImama", razred.MedzlisId);
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina", razred.SkolskaGodinaId);
            return View(razred);
        }

        // GET: Razred/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var razred = await _context.Razredi
                .Include(r => r.Medzlis)
                .Include(r => r.SkolskaGodina)
                .FirstOrDefaultAsync(m => m.RazredId == id);
            if (razred == null)
            {
                return NotFound();
            }

            return View(razred);
        }

        // POST: Razred/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var razred = await _context.Razredi.FindAsync(id);
            _context.Razredi.Remove(razred);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RazredExists(int id)
        {
            return _context.Razredi.Any(e => e.RazredId == id);
        }
    }
}
