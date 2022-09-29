using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_Mekteb.ApDbContext;
using e_Mekteb.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using e_Mekteb.ViewModel;
using System;

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
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId).ToList();
            ViewBag.vjerouciteljId = vjerouciteljId;
            var ucenici = new AplicationUser();
            var tempBiljeske = new List<Biljeska>();
            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);
                var predaje = _context.Predaje.Where(v => v.VjerouciteljId == vjerouciteljId).ToList();
                var biljeske = _context.Biljeske.Where(a=>a.AplicationUserId==id).ToList();
                foreach(var biljeska in biljeske)
                {
                    foreach(var predmetVjeroucitelja in predaje)
                    {
                        if(biljeska.AktivnostId==predmetVjeroucitelja.AktivnostId)
                        tempBiljeske.Add(biljeska);
                    }
                    
                }

            }

            var temp = new List<AplicationUser>();
            foreach ( var user in ucenici.Ucenici)
            {
               foreach(var b in tempBiljeske)
                {
                    if (b.AplicationUserId == user.Id)
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
                       
            var model = new StudentNote
            {
                Biljeske = tempBiljeske,
                Ucenici = temp,
            };
                        
            return View( model);
                        

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
        public async Task<IActionResult> Create()
        {
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId).ToList();
            var ucenici = new AplicationUser();
            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);

            }
            var vjerouciteljaktivnosti = _context.Predaje.Where(a => a.VjerouciteljId == vjerouciteljId);
            ViewData["AktivnostId"] = new SelectList(vjerouciteljaktivnosti, "AktivnostId", "NazivPredmeta");
            ViewData["AplicationUserId"] = new SelectList(ucenici.Ucenici, "AplicationUserId","Email" );
            
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
            var username = HttpContext.User.Identity.Name;
            var vjeroucitelj = await userManager.FindByNameAsync(username);
            var vjerouciteljId = vjeroucitelj.Id;
            var users = (from u in _context.VjerouciteljUcenik
                         where u.VjerouciteljId == vjerouciteljId
                         select u.UcenikId).ToList();
            var ucenici = new AplicationUser();
            foreach (var id in users)
            {
                var user = await userManager.FindByIdAsync(id);
                ucenici.Ucenici.Add(user);

            }
            ViewData["AktivnostId"] = new SelectList(_context.Aktivnosti, "AktivnostId", "Naziv", biljeska.AktivnostId);
            ViewData["AplicationUserId"] = new SelectList(ucenici.Ucenici, "AplicationUserId", "Email",biljeska.AplicationUserId);
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
