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
    public class People1Controller : Controller
    {
        private readonly MainDbContext _context;

        public People1Controller(MainDbContext context)
        {
            _context = context;    
        }

        // GET: People1
        public async Task<IActionResult> Index()
        {
            return View(await _context.People.ToListAsync());
        }

        // GET: People1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .SingleOrDefaultAsync(m => m.ID == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName")] People people)
        {
            if (ModelState.IsValid)
            {
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(people);
        }

        // GET: People1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People.SingleOrDefaultAsync(m => m.ID == id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName")] People people)
        {
            if (id != people.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.ID))
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
            return View(people);
        }

        // GET: People1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .SingleOrDefaultAsync(m => m.ID == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var people = await _context.People.SingleOrDefaultAsync(m => m.ID == id);
            _context.People.Remove(people);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.ID == id);
        }
    }
}
