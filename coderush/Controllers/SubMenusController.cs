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
    [Authorize(Roles = Pages.MainMenu.Submenu.RoleName)]

    public class SubMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubMenus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Submenu.Include(s => s.CreatedUser).Include(s => s.EditedUser).Include(s => s.MenuPath).Include(s => s.Menubar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.Submenu
                .Include(s => s.CreatedUser)
                .Include(s => s.EditedUser)
                .Include(s => s.MenuPath)
                .Include(s => s.Menubar)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // GET: SubMenus/Create
        public IActionResult Create()
        {
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name");
            ViewData["MenubarId"] = new SelectList(_context.Menubar, "Id", "Name");
            return View();
        }

        // POST: SubMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Path,MenuPathId,MenubarId,CreatedUserId,EditUserId,CreateDate,EditDate")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", subMenu.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", subMenu.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", subMenu.MenuPathId);
            ViewData["MenubarId"] = new SelectList(_context.Menubar, "Id", "Name", subMenu.MenubarId);
            return View(subMenu);
        }

        // GET: SubMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.Submenu.SingleOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", subMenu.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", subMenu.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", subMenu.MenuPathId);
            ViewData["MenubarId"] = new SelectList(_context.Menubar, "Id", "Name", subMenu.MenubarId);
            return View(subMenu);
        }

        // POST: SubMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path,MenuPathId,MenubarId,CreatedUserId,EditUserId,CreateDate,EditDate")] SubMenu subMenu)
        {
            if (id != subMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubMenuExists(subMenu.Id))
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
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", subMenu.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", subMenu.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", subMenu.MenuPathId);
            ViewData["MenubarId"] = new SelectList(_context.Menubar, "Id", "Name", subMenu.MenubarId);
            return View(subMenu);
        }

        // GET: SubMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.Submenu
                .Include(s => s.CreatedUser)
                .Include(s => s.EditedUser)
                .Include(s => s.MenuPath)
                .Include(s => s.Menubar)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // POST: SubMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subMenu = await _context.Submenu.SingleOrDefaultAsync(m => m.Id == id);
            _context.Submenu.Remove(subMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubMenuExists(int id)
        {
            return _context.Submenu.Any(e => e.Id == id);
        }
    }
}
