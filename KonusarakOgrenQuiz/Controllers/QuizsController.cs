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
    public class QuizsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            //int urlId = Int32.Parse(id);
            var applicationDbContext = _context.Quiz.Include(q => q.wired);
           // var wiredTitle = _context.Wired.Where(x => x.id == urlId).FirstOrDefault();
           // ViewBag.title = wiredTitle;
            
            return View(await applicationDbContext.ToListAsync());
        }
        //[HttpPost]
        //public IActionResult Create(IEnumerable<Questions> listOfQuestions)
        //{
           
        //    return View();
        //}

        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.wired)
                .FirstOrDefaultAsync(m => m.id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizs/Create
        public IActionResult Create(string id)
        {
            if (id != null) { 
                int wid = Int32.Parse(id);
                var wiredT = _context.Wired.Where(x => x.id == wid).ToList();
            
                if (wiredT != null)
                    ViewData["wiredId"] = new SelectList(wiredT, "id", "titles");
            }
            else if (id == null)
            {
                var wiredT = _context.Wired.ToList();
                if (wiredT != null)
                {
                    ViewData["wiredId"] = new SelectList(wiredT, "id", "titles");
                }
            }
            else
            {
                return RedirectToAction("Index");             
            }
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,wiredId")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["wiredId"] = new SelectList(_context.Wired, "id", "titles", quiz.wiredId);
            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            ViewData["wiredId"] = new SelectList(_context.Wired, "id", "id", quiz.wiredId);
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,wiredId")] Quiz quiz)
        {
            if (id != quiz.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.id))
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
            ViewData["wiredId"] = new SelectList(_context.Wired, "id", "id", quiz.wiredId);
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.wired)
                .FirstOrDefaultAsync(m => m.id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);
            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.id == id);
        }
    }
}
