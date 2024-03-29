﻿using System;
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
    public class AktivnostController : Controller
    {
        private readonly e_MektebDbContext _context;

        public AktivnostController(e_MektebDbContext context)
        {
            _context = context;
        }

        // GET: Aktivnost
        public async Task<IActionResult> Index()
        {
            var e_MektebDbContext = _context.Aktivnosti.Include(a => a.SkolskaGodina);
            return View(await e_MektebDbContext.ToListAsync());
        }

        // GET: Aktivnost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivnost = await _context.Aktivnosti
                .Include(a => a.SkolskaGodina)
                .FirstOrDefaultAsync(m => m.AktivnostId == id);
            if (aktivnost == null)
            {
                return NotFound();
            }

            return View(aktivnost);
        }

        // GET: Aktivnost/Create
        public IActionResult Create()
        {
           var enumTipAktivnosti= Enum.GetValues(typeof(TipAktivnosti)).Cast<TipAktivnosti>().Select(v => v.ToString()).ToList();
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina");
            ViewData["AktivnostId"] = new SelectList(enumTipAktivnosti);
            return View();
        }

        // POST: Aktivnost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AktivnostId,SkolskaGodinaId,Naziv,TipAktivnosti")] Aktivnost aktivnost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktivnost);
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina", aktivnost.SkolskaGodinaId);
            var enumTipAktivnosti = Enum.GetValues(typeof(TipAktivnosti)).Cast<TipAktivnosti>().Select(v => v.ToString()).ToList();
            ViewData["AktivnostId"] = new SelectList(enumTipAktivnosti);

            return View(aktivnost);
        }

        // GET: Aktivnost/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivnost = await _context.Aktivnosti.FindAsync(id);
            if (aktivnost == null)
            {
                return NotFound();
            }
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina", aktivnost.SkolskaGodinaId);
            var enumTipAktivnosti = Enum.GetValues(typeof(TipAktivnosti)).Cast<TipAktivnosti>().Select(v => v.ToString()).ToList();
            ViewData["AktivnostId"] = new SelectList(enumTipAktivnosti);
            return View(aktivnost);
        }

        // POST: Aktivnost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AktivnostId,SkolskaGodinaId,Naziv,TipAktivnosti")] Aktivnost aktivnost)
        {
            if (id != aktivnost.AktivnostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktivnost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktivnostExists(aktivnost.AktivnostId))
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
            ViewData["SkolskaGodinaId"] = new SelectList(_context.SkolskeGodine, "SkolskaGodinaId", "Godina", aktivnost.SkolskaGodinaId);
            return View(aktivnost);
        }

        // GET: Aktivnost/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivnost = await _context.Aktivnosti
                .Include(a => a.SkolskaGodina)
                .FirstOrDefaultAsync(m => m.AktivnostId == id);
            if (aktivnost == null)
            {
                return NotFound();
            }

            return View(aktivnost);
        }

        // POST: Aktivnost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktivnost = await _context.Aktivnosti.FindAsync(id);
            _context.Aktivnosti.Remove(aktivnost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktivnostExists(int id)
        {
            return _context.Aktivnosti.Any(e => e.AktivnostId == id);
        }
    }
}
