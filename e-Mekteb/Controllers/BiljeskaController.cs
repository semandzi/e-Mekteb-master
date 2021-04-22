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
    public class BiljeskaController : Controller
    {
        private readonly e_MektebDbContext _context;
        private readonly UserManager<AplicationUser> userManager;

        public BiljeskaController(e_MektebDbContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            this.userManager=userManager;
        }

        // GET: Biljeska
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.Biljeske.Include(b => b.Aktivnost).Include(b=>b.AplicationUser);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: Biljeska/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biljeska = await _context.Biljeske
                .Include(b => b.Aktivnost)
                .FirstOrDefaultAsync(m => m.BiljeskaId == id);
            if (biljeska == null)
            {
                return NotFound();
            }

            return View(biljeska);
        }

        // GET: Biljeska/Create
        public IActionResult Create()
        {
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv");
            ViewData["AplicationUserId"] = new SelectList(_context.Users, "AplicationUserId","Email" );
            
            return View();
        }

        // POST: Biljeska/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BiljeskaId,Datum,AplicationUserId,AktivnostId,Biljeske")] Biljeska biljeska)
        {
            
            
            if (ModelState.IsValid)
            {
                _context.Add(biljeska);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", biljeska.AktivnostId);
            ViewData["AplicationUserId"] = new SelectList(_context.Users, "AplicationUserId", "Email",biljeska.AplicationUserId);
            return View(biljeska);
        }

        // GET: Biljeska/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biljeska = await _context.Biljeske.FindAsync(id);
            if (biljeska == null)
            {
                return NotFound();
            }
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", biljeska.AktivnostId);

            return View(biljeska);
        }

        // POST: Biljeska/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BiljeskaId,Datum,AplicationUserId,AktivnostId,Biljeske")] Biljeska biljeska)
        {
            if (id != biljeska.BiljeskaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biljeska);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiljeskaExists(biljeska.BiljeskaId))
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
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", biljeska.AktivnostId);
            return View(biljeska);
        }

        // GET: Biljeska/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biljeska = await _context.Biljeske
                .Include(b => b.Aktivnost)
                .FirstOrDefaultAsync(m => m.BiljeskaId == id);
            if (biljeska == null)
            {
                return NotFound();
            }

            return View(biljeska);
        }

        // POST: Biljeska/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biljeska = await _context.Biljeske.FindAsync(id);
            _context.Biljeske.Remove(biljeska);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiljeskaExists(int id)
        {
            return _context.Biljeske.Any(e => e.BiljeskaId == id);
        }
    }
}
