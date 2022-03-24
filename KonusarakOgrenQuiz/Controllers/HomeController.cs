using HtmlAgilityPack;
using KonusarakOgrenQuiz.Data;
using KonusarakOgrenQuiz.Models;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Wired> wired = new List<Wired>();

            var web = new HtmlWeb();
            var doc = web.Load("https://www.wired.com/most-recent/");

            for (int i = 0; i < 5; i++)
            {
                var dbWired = _context.Wired.Where(x=>x.id==i+1).FirstOrDefault();
                var item = doc.DocumentNode.SelectNodes("//div[@class='archive-item-component__info']")[i];

                string title = item.SelectSingleNode(".//h2").InnerText.Trim();
                string shortDet = item.SelectSingleNode(".//p").InnerText.Trim();
                string href = item.SelectSingleNode(".//a[@class='archive-item-component__link']").GetAttributeValue("href", null).ToString();
                string details = string.Empty;
                var docPara = web.Load("https://www.wired.com" + href);
                var paragraph = docPara.DocumentNode.SelectNodes("//div[@class='body__inner-container']");
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
            
            return View(wired);
        }

        public IActionResult Privacy()
        {
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
