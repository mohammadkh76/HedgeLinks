using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;

namespace coderush.Controllers
{
    public class MenuPathsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuPathsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuPaths
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuPath.ToListAsync());
        }

        // GET: MenuPaths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPath = await _context.MenuPath
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuPath == null)
            {
                return NotFound();
            }

            return View(menuPath);
        }

        // GET: MenuPaths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuPaths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,FilePath,PageName")] MenuPath menuPath)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuPath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuPath);
        }

        // GET: MenuPaths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPath = await _context.MenuPath.SingleOrDefaultAsync(m => m.Id == id);
            if (menuPath == null)
            {
                return NotFound();
            }
            return View(menuPath);
        }

        // POST: MenuPaths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Description,FilePath,PageName")] MenuPath menuPath)
        {
            if (id != menuPath.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuPath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuPathExists(menuPath.Id))
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
            return View(menuPath);
        }

        // GET: MenuPaths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuPath = await _context.MenuPath
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuPath == null)
            {
                return NotFound();
            }

            return View(menuPath);
        }

        // POST: MenuPaths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var menuPath = await _context.MenuPath.SingleOrDefaultAsync(m => m.Id == id);
            _context.MenuPath.Remove(menuPath);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuPathExists(int? id)
        {
            return _context.MenuPath.Any(e => e.Id == id);
        }
    }
}
