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
    public class AnswersController : Controller
    {
        private readonly MainDbContext _context;

        public AnswersController(MainDbContext context)
        {
            _context = context;    
        }

        // GET: Answers
        public async Task<IActionResult> Index(int? id)
        {
            var mainDbContext = await _context.Answer.Include(a => a.AnswerType).Include(b=>b.Questionnaire).Include(c=>c.Questionnaire.Question).Where(F=>F.QuestionnaireId == id).ToListAsync();
            return PartialView("_AnswerList",  mainDbContext); //View(await mainDbContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.Include(a => a.AnswerType).SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Answer model)
        {   if (_context.Question.Last().IsDependent)
                model.NextQuestionId = _context.Question.Last().Id;
            else
                model.NextQuestionId = 0;

            model.QuestionnaireId = _context.Questionnaire.Last().Id;
            if (ModelState.IsValid)
            {
                _context.Answer.Add(model);
                _context.SaveChanges();
                return RedirectToAction("IndexOfThis", new { id = model.Id });
            }
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText", model.AnswerTypeId);
            return Json(model);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText", answer.AnswerTypeId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Answer model)
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
                    if (!AnswerExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText", model.AnswerTypeId);
            return View(model);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.Include(a => a.AnswerType).SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(e => e.Id == id);
        }

        public string[] IndexOfThis(int? id)
        {
            var ab = _context.Answer.Last();
            var onebefore = _context.Answer.SingleOrDefault(a => a.Id == id-1);
            var count = _context.Answer.Count();
            if (ab.NextQuestionId == onebefore.NextQuestionId)
            {
                ab.NextQuestionId = 0;
            }

            string[] modelStr = new string[2];
            modelStr[0] = ab.AnswerText;

            if (ab.NextQuestionId!=0)
            {
                modelStr[1] = _context.Question.SingleOrDefault(q => q.Id == ab.NextQuestionId).QuestionTitle;
            }
            else
            {
                modelStr[1] = "";
            }

            return modelStr; 
        }


    }
}
