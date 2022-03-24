using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KonusarakOgrenQuiz.Data;
using KonusarakOgrenQuiz.Models;

namespace KonusarakOgrenQuiz.Controllers
{
    public class QuestionListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuestionLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.questionList.Include(q => q.quiz);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: QuestionLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionList = await _context.questionList
                .Include(q => q.quiz)
                .FirstOrDefaultAsync(m => m.id == id);
            if (questionList == null)
            {
                return NotFound();
            }

            return View(questionList);
        }
        public async Task<IActionResult> AddQuestion(QuestionList questionList)
        {
            
            if (ModelState.IsValid)
            {

                foreach(var items in questionList.questions)
                {
                    _context.Add(items);
                }
                _context.Add(questionList);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(questionList);
        }
        // GET: QuestionLists/Create
        public IActionResult Create()
        {
            ViewData["quizId"] = new SelectList(_context.Quiz, "id", "id");
            return View();
        }

        // POST: QuestionLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,quizId")] QuestionList questionList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["quizId"] = new SelectList(_context.Quiz, "id", "id", questionList.quizId);
            return View(questionList);
        }

        // GET: QuestionLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionList = await _context.questionList.FindAsync(id);
            if (questionList == null)
            {
                return NotFound();
            }
            ViewData["quizId"] = new SelectList(_context.Quiz, "id", "id", questionList.quizId);
            return View(questionList);
        }

        // POST: QuestionLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,quizId")] QuestionList questionList)
        {
            if (id != questionList.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionListExists(questionList.id))
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
            ViewData["quizId"] = new SelectList(_context.Quiz, "id", "id", questionList.quizId);
            return View(questionList);
        }

        // GET: QuestionLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionList = await _context.questionList
                .Include(q => q.quiz)
                .FirstOrDefaultAsync(m => m.id == id);
            if (questionList == null)
            {
                return NotFound();
            }

            return View(questionList);
        }

        // POST: QuestionLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionList = await _context.questionList.FindAsync(id);
            _context.questionList.Remove(questionList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionListExists(int id)
        {
            return _context.questionList.Any(e => e.id == id);
        }
    }
}
