using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HedgeLinks.Data;
using HedgeLinks.Models;

namespace HedgeLinks.Controllers
{
    public class ThirdSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThirdSectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThirdSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThirdSection.ToListAsync());
        }

        // GET: ThirdSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thirdSection = await _context.ThirdSection
                .SingleOrDefaultAsync(m => m.Id == id);
            if (thirdSection == null)
            {
                return NotFound();
            }

            return View(thirdSection);
        }

        // GET: ThirdSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThirdSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Subtitle,FilePath")] ThirdSection thirdSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thirdSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thirdSection);
        }

        // GET: ThirdSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thirdSection = await _context.ThirdSection.SingleOrDefaultAsync(m => m.Id == id);
            if (thirdSection == null)
            {
                return NotFound();
            }
            return View(thirdSection);
        }

        // POST: ThirdSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Subtitle,FilePath")] ThirdSection thirdSection)
        {
            if (id != thirdSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thirdSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThirdSectionExists(thirdSection.Id))
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
            return View(thirdSection);
        }

        // GET: ThirdSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thirdSection = await _context.ThirdSection
                .SingleOrDefaultAsync(m => m.Id == id);
            if (thirdSection == null)
            {
                return NotFound();
            }

            return View(thirdSection);
        }

        // POST: ThirdSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thirdSection = await _context.ThirdSection.SingleOrDefaultAsync(m => m.Id == id);
            _context.ThirdSection.Remove(thirdSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThirdSectionExists(int id)
        {
            return _context.ThirdSection.Any(e => e.Id == id);
        }
    }
}
