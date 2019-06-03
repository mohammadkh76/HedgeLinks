using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HedgeLinks.Data;
using HedgeLinks.Models;
using Microsoft.AspNetCore.Authorization;

namespace HedgeLinks.Controllers
{
    [Authorize(Roles = Pages.MainMenu.JobIndustry.RoleName)]

    public class JobIndustriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobIndustriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobIndustries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobIndustries.Include(j => j.CreatedUser).Include(j => j.EditedUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobIndustries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobIndustry = await _context.JobIndustries
                .Include(j => j.CreatedUser)
                .Include(j => j.EditedUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobIndustry == null)
            {
                return NotFound();
            }

            return View(jobIndustry);
        }

        // GET: JobIndustries/Create
        public IActionResult Create()
        {
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: JobIndustries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Id,CreatedUserId,EditUserId,CreateDate,EditDate")] JobIndustry jobIndustry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobIndustry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", jobIndustry.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", jobIndustry.EditUserId);
            return View(jobIndustry);
        }

        // GET: JobIndustries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobIndustry = await _context.JobIndustries.SingleOrDefaultAsync(m => m.Id == id);
            if (jobIndustry == null)
            {
                return NotFound();
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", jobIndustry.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", jobIndustry.EditUserId);
            return View(jobIndustry);
        }

        // POST: JobIndustries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Id,CreatedUserId,EditUserId,CreateDate,EditDate")] JobIndustry jobIndustry)
        {
            if (id != jobIndustry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobIndustry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobIndustryExists(jobIndustry.Id))
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
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", jobIndustry.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", jobIndustry.EditUserId);
            return View(jobIndustry);
        }

        // GET: JobIndustries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobIndustry = await _context.JobIndustries
                .Include(j => j.CreatedUser)
                .Include(j => j.EditedUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobIndustry == null)
            {
                return NotFound();
            }

            return View(jobIndustry);
        }

        // POST: JobIndustries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobIndustry = await _context.JobIndustries.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobIndustries.Remove(jobIndustry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobIndustryExists(int id)
        {
            return _context.JobIndustries.Any(e => e.Id == id);
        }
    }
}
