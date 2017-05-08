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
    public class Answers1Controller : Controller
    {
        private readonly MainDbContext _context;

        public Answers1Controller(MainDbContext context)
        {
            _context = context;    
        }

        // GET: Answers1
        public async Task<IActionResult> Index()
        {
            var mainDbContext = _context.Answer.Include(a => a.AnswerType).Include(a => a.Questionnaire);
            return View(await mainDbContext.ToListAsync());
        }

        // GET: Answers1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .Include(a => a.AnswerType)
                .Include(a => a.Questionnaire)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers1/Create
        public IActionResult Create()
        {
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText");
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaire, "Id", "Id");
            return View();
        }

        // POST: Answers1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionnaireId,AnswerTypeId,AnswerText,NextQuestionId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText", answer.AnswerTypeId);
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaire, "Id", "Id", answer.QuestionnaireId);
            return View(answer);
        }

        // GET: Answers1/Edit/5
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
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaire, "Id", "Id", answer.QuestionnaireId);
            return View(answer);
        }

        // POST: Answers1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionnaireId,AnswerTypeId,AnswerText,NextQuestionId")] Answer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
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
            ViewData["AnswerTypeId"] = new SelectList(_context.AnswerType, "Id", "AnswerTypeText", answer.AnswerTypeId);
            ViewData["QuestionnaireId"] = new SelectList(_context.Questionnaire, "Id", "Id", answer.QuestionnaireId);
            return View(answer);
        }

        // GET: Answers1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .Include(a => a.AnswerType)
                .Include(a => a.Questionnaire)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers1/Delete/5
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
    }
}
