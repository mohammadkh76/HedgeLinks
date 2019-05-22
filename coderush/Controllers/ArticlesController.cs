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
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Article.Include(a => a.ArticleTopic).Include(a => a.CreatedUser).Include(a => a.EditedUser).Include(a => a.Menupath);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.ArticleTopic)
                .Include(a => a.CreatedUser)
                .Include(a => a.EditedUser)
                .Include(a => a.Menupath)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["ArticleTopicId"] = new SelectList(_context.ArticleTopic, "Id", "Id");
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorName,Title,Description,Date,keyword,ArticleTopicId,MenuPathId,ExternalLink,isShow,CreatedUserId,EditUserId,CreateDate,EditDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleTopicId"] = new SelectList(_context.ArticleTopic, "Id", "Id", article.ArticleTopicId);
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", article.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", article.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", article.MenuPathId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["ArticleTopicId"] = new SelectList(_context.ArticleTopic, "Id", "Id", article.ArticleTopicId);
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", article.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", article.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", article.MenuPathId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorName,Title,Description,Date,keyword,ArticleTopicId,MenuPathId,ExternalLink,isShow,CreatedUserId,EditUserId,CreateDate,EditDate")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            ViewData["ArticleTopicId"] = new SelectList(_context.ArticleTopic, "Id", "Id", article.ArticleTopicId);
            ViewData["CreatedUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", article.CreatedUserId);
            ViewData["EditUserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", article.EditUserId);
            ViewData["MenuPathId"] = new SelectList(_context.MenuPath, "Id", "Name", article.MenuPathId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.ArticleTopic)
                .Include(a => a.CreatedUser)
                .Include(a => a.EditedUser)
                .Include(a => a.Menupath)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.SingleOrDefaultAsync(m => m.Id == id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
