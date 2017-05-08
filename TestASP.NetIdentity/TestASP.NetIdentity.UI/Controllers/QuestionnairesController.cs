using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestASP.NetIdentity.DataAccess;
using TestASP.NetIdentity.DomainEntities;

namespace TestASP.NetIdentity.UI.Controllers
{
    public class QuestionnairesController : Controller
    {
        private readonly MainDbContext _context;

        public QuestionnairesController(MainDbContext context)
        {
            _context = context;    
        }

        // GET: Questionnaires
        public async Task<IActionResult> Index(int? id)
        {
            var mainDbContext = _context.Questionnaire.Include(q => q.Advertisement).Include(q => q.Question).Where(F => F.AdId == id);
            return View(await mainDbContext.ToListAsync());
        }


        // GET: Questionnaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaire.Include(q => q.Advertisement).Include(q => q.Question).SingleOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // GET: Questionnaires/Create
        public IActionResult Create(int? adid)
        {
            ViewData["AdId"] = adid; //ViewData["AdId"] = new SelectList(_context.Advertisement, "Id", "AdTitle");
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "QuestionTitle");
            return PartialView("_QuestionnaireAdd"); //View();
        }

        // POST: Questionnaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Questionnaire model)
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

        // GET: Questionnaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaire.SingleOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }
            ViewData["AdId"] = questionnaire.AdId;
            //ViewData["AdId"] = new SelectList(_context.Advertisement, "Id", "AdTitle", questionnaire.AdId);
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "QuestionTitle", questionnaire.QuestionId);
            return PartialView("_QuestionnaireEdit", questionnaire); //View(questionnaire);
        }

        // POST: Questionnaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Questionnaire model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = model.AdId });
            }
            ViewData["AdId"] = new SelectList(_context.Advertisement, "Id", "AdTitle", model.AdId);
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "QuestionTitle", model.QuestionId);
            return View(model);
        }

        // GET: Questionnaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaire.Include(q => q.Advertisement).Include(q => q.Question).SingleOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaire = await _context.Questionnaire.SingleOrDefaultAsync(m => m.Id == id);
            _context.Questionnaire.Remove(questionnaire);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool QuestionnaireExists(int id)
        {
            return _context.Questionnaire.Any(e => e.Id == id);
        }



        public IActionResult MakeQuestionnaire(int? id)
        {
            ViewData["AdId"] = id;
            ViewData["QTypeId"] = new SelectList(_context.QuestionType, "Id", "QTypeTitle");
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText");
            ViewData["QuestionId"] = _context.Questionnaire.Last().QuestionId;
            ViewData["Questions"] = new SelectList(_context.Question, "Id", "QuestionTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeQuestionnaire(int? id, Questionnaire model)
        {
            model.QuestionId = _context.Question.Last().Id;
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return Json(model);
            }
            
            ViewData["QuestionId"] = new SelectList(_context.Question, "Id", "QuestionTitle", model.QuestionId);
            return Json(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAnswer( Answer model)
        {
            if (_context.Question.Last().IsDependent)
                model.NextQuestionId = _context.Question.Last().Id;
            else
                model.NextQuestionId = 0;

            model.QuestionnaireId = _context.Questionnaire.Last().Id;
            if (ModelState.IsValid)
            {
                _context.Answer.Add(model);
                _context.SaveChanges();
                return Json(model);
            }
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText", model.AnswerTypeId);
            return Json(model);
        }

    }
}
