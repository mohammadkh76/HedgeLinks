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
    public class ComercialTipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComercialTipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComercialTips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComercialTips.Include(c => c.CreatedUser).Include(c => c.EditedUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ComercialTips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comercialTips = await _context.ComercialTips
                .Include(c => c.CreatedUser)
                .Include(c => c.EditedUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comercialTips == null)
            {
                return NotFound();
            }

            return View(comercialTips);
        }

        // GET: ComercialTips/Create
        public IActionResult Create()
        {
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: ComercialTips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Subtitle,Keyword,FilePath,CreatedUserId,EditUserId,CreateDate,EditDate")] ComercialTips comercialTips)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comercialTips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comercialTips.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comercialTips.EditUserId);
            return View(comercialTips);
        }

        // GET: ComercialTips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comercialTips = await _context.ComercialTips.SingleOrDefaultAsync(m => m.Id == id);
            if (comercialTips == null)
            {
                return NotFound();
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comercialTips.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comercialTips.EditUserId);
            return View(comercialTips);
        }

        // POST: ComercialTips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Subtitle,Keyword,FilePath,CreatedUserId,EditUserId,CreateDate,EditDate")] ComercialTips comercialTips)
        {
            if (id != comercialTips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comercialTips);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComercialTipsExists(comercialTips.Id))
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
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comercialTips.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", comercialTips.EditUserId);
            return View(comercialTips);
        }

        // GET: ComercialTips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comercialTips = await _context.ComercialTips
                .Include(c => c.CreatedUser)
                .Include(c => c.EditedUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comercialTips == null)
            {
                return NotFound();
            }

            return View(comercialTips);
        }

        // POST: ComercialTips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comercialTips = await _context.ComercialTips.SingleOrDefaultAsync(m => m.Id == id);
            _context.ComercialTips.Remove(comercialTips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComercialTipsExists(int id)
        {
            return _context.ComercialTips.Any(e => e.Id == id);
        }
    }
}
