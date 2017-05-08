using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestASP.NetIdentity.DomainEntities;
using TestASP.NetIdentity.DataAccess;

namespace TestASP.NetIdentity.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainDbContext _context;

        public HomeController(MainDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About(int? id)
        {
            ViewData["AdId"] = id; 
            ViewData["QTypeId"] = new SelectList(_context.QuestionType, "Id", "QTypeTitle");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> About(Questionnaire model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = model.AdId });
            }
            //ViewData["AdId"] = new SelectList(_context.Advertisement, "Id", "AdTitle", questionnaire.AdId);
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "QuestionTitle", model.QuestionId);
            return View(model);
        }

        public IActionResult SearchResult(Advertisement ad)
        {
            var model = _context.Advertisement.ToList();
            return PartialView("_AdvertisementList", model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
