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
    public class PrisutnostController : Controller
    {
        private readonly e_MektebDbContext _context;
        private readonly UserManager<AplicationUser> userManager;

        public PrisutnostController(e_MektebDbContext context, UserManager<AplicationUser> userManager)
        {
            this.userManager = userManager;
            _context = context;
        }

        // GET: Prisutnost
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId);
            var ucenici = new AplicationUser();
            var tempPrisutnosti = new List<Prisutnost>();

            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);
                var prisutnosti = _context.Prisutnosti.Where(a => a.AplicationUserId == id);
                foreach (var prisutnost in prisutnosti)
                {
                    tempPrisutnosti.Add(prisutnost);
                }
            }

            var temp = new List<AplicationUser>();
            foreach (var user in ucenici.Ucenici)
            {
                foreach (var p in tempPrisutnosti)
                {
                    if (p.AplicationUserId == user.Id)
                    {
                        if (temp.Contains(user))
                        {
                            continue;
                        }
                        else
                        {
                            temp.Add(user);
                        }
                    }
                }
            }

            var model = new PrisutnostiUcenik
            {
                Prisutnosti = tempPrisutnosti,
                Ucenici = temp

            };

            return View(model);


        }

        // GET: Prisutnost/Create
        public  async Task<IActionResult> Create()
        {
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId);
            var ucenici = new AplicationUser();
            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);

            }


            var vjerouciteljaktivnosti = _context.Predaje.Where(p => p.VjerouciteljId == vjerouciteljId);

            ViewData["AktivnostId"] = new SelectList(vjerouciteljaktivnosti, "AktivnostId", "NazivPredmeta");
            ViewData["AplicationUserId"] = new SelectList(ucenici.Ucenici, "AplicationUserId", "Email");
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
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId);
            var ucenici = new AplicationUser();
            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);

            }
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", prisutnost.AktivnostId);
            ViewData["AplicationUserId"] = new SelectList(ucenici.Ucenici, "AplicationUserId", "ImeIPrezime", prisutnost.AplicationUserId);
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
        // GET: Prisutnost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisutnost = await _context.Prisutnosti
                .Include(b => b.Aktivnost)
                .FirstOrDefaultAsync(m => m.PrisutnostId == id);
            if (prisutnost == null)
            {
                return NotFound();
            }

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
