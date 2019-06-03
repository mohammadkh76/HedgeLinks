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
    [Authorize(Roles = Pages.MainMenu.Menubar.RoleName)]

    public class MenubarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenubarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Menubars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Menubar.Include(m => m.CreatedUser).Include(m => m.EditedUser).Include(m => m.MenuPath);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Menubars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menubar = await _context.Menubar
                .Include(m => m.CreatedUser)
                .Include(m => m.EditedUser)
                .Include(m => m.MenuPath)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menubar == null)
            {
                return NotFound();
            }

            return View(menubar);
        }

        // GET: Menubars/Create
        public IActionResult Create()
        {
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name");
            return View();
        }

        // POST: Menubars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Path,MenuPathId,CreatedUserId,EditUserId,CreateDate,EditDate")] Menubar menubar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menubar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", menubar.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", menubar.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", menubar.MenuPathId);
            return View(menubar);
        }

        // GET: Menubars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menubar = await _context.Menubar.SingleOrDefaultAsync(m => m.Id == id);
            if (menubar == null)
            {
                return NotFound();
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", menubar.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", menubar.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", menubar.MenuPathId);
            return View(menubar);
        }

        // POST: Menubars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path,MenuPathId,CreatedUserId,EditUserId,CreateDate,EditDate")] Menubar menubar)
        {
            if (id != menubar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menubar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenubarExists(menubar.Id))
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
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", menubar.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", menubar.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", menubar.MenuPathId);
            return View(menubar);
        }

        // GET: Menubars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menubar = await _context.Menubar
                .Include(m => m.CreatedUser)
                .Include(m => m.EditedUser)
                .Include(m => m.MenuPath)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menubar == null)
            {
                return NotFound();
            }

            return View(menubar);
        }

        // POST: Menubars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menubar = await _context.Menubar.SingleOrDefaultAsync(m => m.Id == id);
            _context.Menubar.Remove(menubar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenubarExists(int id)
        {
            return _context.Menubar.Any(e => e.Id == id);
        }
    }
}
