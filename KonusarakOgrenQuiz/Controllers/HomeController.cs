using HtmlAgilityPack;
using KonusarakOgrenQuiz.Data;
using KonusarakOgrenQuiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = User.Identity;
            var quizList = _context.Quiz.Include(x => x.wired).ToList();
            //await _userManager.AddToRoleAsync((IdentityUser)user, Models.Roles.Admin.ToString());
            return View(quizList);
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Privacy()
        {
            List<Wired> wired = new List<Wired>();

            var web = new HtmlWeb();
            var doc = web.Load("https://www.wired.com/most-recent/");
            var sW = doc.DocumentNode.SelectSingleNode(".//div[@class='summary-list__items']");

            for (int i = 0; i < 5; i++)
            {
                var dbWired = _context.Wired.Where(x => x.id == i + 1).FirstOrDefault();
                var item = doc.DocumentNode.SelectNodes("//div[contains(@class,'summary-item__content')]")[i];

                string title = item.SelectSingleNode(".//h3[contains(@class,'summary-item__hed')]").InnerText.Trim();
                string shortDet = item.SelectSingleNode(".//div[contains(@class,'summary-item__dek')]").InnerText.Trim();
                string href = item.SelectSingleNode(".//a[contains(@class,'summary-item__hed-link')]").GetAttributeValue("href", null).ToString();
                string details = string.Empty;
                var docPara = web.Load("https://www.wired.com" + href);
                var paragraph = docPara.DocumentNode.SelectNodes("//div[contains(@class,'body__container')]");
                var parahraphDetails = paragraph[0].SelectNodes("//p");
                for (int k = 5; k < parahraphDetails.Count() - 3; k++)
                {
                    details += parahraphDetails[k].InnerText;
                }
                dbWired.id = i + 1;
                dbWired.details = details;
                dbWired.titles = title;
                dbWired.smallText = shortDet;
                _context.Update(dbWired);

            }
            await _context.SaveChangesAsync();
            var mainPage = _context.Wired.ToList();
            return View(mainPage);
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<Wired> wired)
        {            
            if (ModelState.IsValid)
            {
                foreach(var item in wired)
                {
                     _context.Update(item);

                }
                await _context.SaveChangesAsync();                
            }
            return View(wired);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
