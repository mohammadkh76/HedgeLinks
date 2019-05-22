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
    public class ArticleTopicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleTopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArticleTopics
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticleTopic.ToListAsync());
        }

        // GET: ArticleTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleTopic = await _context.ArticleTopic
                .SingleOrDefaultAsync(m => m.Id == id);
            if (articleTopic == null)
            {
                return NotFound();
            }

            return View(articleTopic);
        }

        // GET: ArticleTopics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticleTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] ArticleTopic articleTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articleTopic);
        }

        // GET: ArticleTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleTopic = await _context.ArticleTopic.SingleOrDefaultAsync(m => m.Id == id);
            if (articleTopic == null)
            {
                return NotFound();
            }
            return View(articleTopic);
        }

        // POST: ArticleTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] ArticleTopic articleTopic)
        {
            if (id != articleTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleTopicExists(articleTopic.Id))
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
            return View(articleTopic);
        }

        // GET: ArticleTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleTopic = await _context.ArticleTopic
                .SingleOrDefaultAsync(m => m.Id == id);
            if (articleTopic == null)
            {
                return NotFound();
            }

            return View(articleTopic);
        }

        // POST: ArticleTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleTopic = await _context.ArticleTopic.SingleOrDefaultAsync(m => m.Id == id);
            _context.ArticleTopic.Remove(articleTopic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleTopicExists(int id)
        {
            return _context.ArticleTopic.Any(e => e.Id == id);
        }
    }
}
