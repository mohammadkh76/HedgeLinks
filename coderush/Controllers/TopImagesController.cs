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
    public class TopImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TopImages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TopImage.Include(t => t.CreatedUser).Include(t => t.EditedUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TopImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topImage = await _context.TopImage
                .Include(t => t.CreatedUser)
                .Include(t => t.EditedUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (topImage == null)
            {
                return NotFound();
            }

            return View(topImage);
        }

        // GET: TopImages/Create
        public IActionResult Create()
        {
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: TopImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageTitle,ImageSubtitle,Keyword,FilePath,CreatedUserId,EditUserId,CreateDate,EditDate")] TopImage topImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", topImage.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", topImage.EditUserId);
            return View(topImage);
        }

        // GET: TopImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topImage = await _context.TopImage.SingleOrDefaultAsync(m => m.Id == id);
            if (topImage == null)
            {
                return NotFound();
            }
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", topImage.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", topImage.EditUserId);
            return View(topImage);
        }

        // POST: TopImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageTitle,ImageSubtitle,Keyword,FilePath,CreatedUserId,EditUserId,CreateDate,EditDate")] TopImage topImage)
        {
            if (id != topImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopImageExists(topImage.Id))
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
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", topImage.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", topImage.EditUserId);
            return View(topImage);
        }

        // GET: TopImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topImage = await _context.TopImage
                .Include(t => t.CreatedUser)
                .Include(t => t.EditedUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (topImage == null)
            {
                return NotFound();
            }

            return View(topImage);
        }

        // POST: TopImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topImage = await _context.TopImage.SingleOrDefaultAsync(m => m.Id == id);
            _context.TopImage.Remove(topImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopImageExists(int id)
        {
            return _context.TopImage.Any(e => e.Id == id);
        }
    }
}
