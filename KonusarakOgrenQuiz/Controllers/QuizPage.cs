using KonusarakOgrenQuiz.Data;
using KonusarakOgrenQuiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Controllers
{
    [Authorize]
    public class QuizPage : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizPage(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var quizList = _context.Quiz.Include(x=>x.wired);

            List<string> wiredN = new List<string>();
            List<int> quizIds = new List<int>();

            foreach(var item in quizList)
            {
                wiredN.Add(item.wired.titles);
                quizIds.Add(item.id);
            }
            ViewBag.List = wiredN;
            ViewBag.IdList = quizIds;

            Container container = new Container();
            container.quiz = quizList;

            return View(container);
        }


        public async Task<IActionResult> Quiz(string id)
        {
            int qid = Int32.Parse(id);
            var questionListId = _context.questionList.Where(x => x.quizId == qid).FirstOrDefault();
            if (questionListId != null)
            {
                var questionList = _context.questions.Where(x => x.questionListId == questionListId.id).ToList();
                var wiredT = _context.Quiz.Where(y => y.id == qid).Include(x => x.wired);           

                Container container = new Container();
                container.quiz = wiredT;
                container.questions = questionList;
                var forBag = wiredT.FirstOrDefault();
                var title = forBag.wired.titles;
                var details = forBag.wired.details;
                ViewBag.WTitle = title;
                ViewBag.WDetail = details;
                return View(container);

            }
            else
            {
                return View();
            }

        }
    }
}
