using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KonusarakOgrenQuiz.Data;
using KonusarakOgrenQuiz.Models;
using Microsoft.AspNetCore.Authorization;

namespace KonusarakOgrenQuiz.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class WiredsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WiredsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wireds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wired.ToListAsync());
        }

        // GET: Wireds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wired = await _context.Wired
                .FirstOrDefaultAsync(m => m.id == id);
            if (wired == null)
            {
                return NotFound();
            }

            return View(wired);
        }

        // GET: Wireds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wireds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,titles,smallText,details")] Wired wired)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wired);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wired);
        }

        // GET: Wireds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wired = await _context.Wired.FindAsync(id);
            if (wired == null)
            {
                return NotFound();
            }
            return View(wired);
        }

        // POST: Wireds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,titles,smallText,details")] Wired wired)
        {
            if (id != wired.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wired);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WiredExists(wired.id))
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
            return View(wired);
        }

        // GET: Wireds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wired = await _context.Wired
                .FirstOrDefaultAsync(m => m.id == id);
            if (wired == null)
            {
                return NotFound();
            }

            return View(wired);
        }

        // POST: Wireds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wired = await _context.Wired.FindAsync(id);
            _context.Wired.Remove(wired);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WiredExists(int id)
        {
            return _context.Wired.Any(e => e.id == id);
        }
    }
}
