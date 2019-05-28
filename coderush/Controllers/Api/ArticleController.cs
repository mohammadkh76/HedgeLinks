using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgeLinks.Data;
using HedgeLinks.Models;
using HedgeLinks.Models.RESTViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    public class ArticleController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ArticleController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/User
        [HttpPost]
        [Route("api/Article/GetAll/")]
        public IActionResult PostArticle([FromBody] PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);

            var Items = _context.Article.Include(x => x.CreatedUser).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();

            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }
     
      
        [HttpGet("api/Article/Delete/{id}")]

        public IActionResult DelMenubar(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.Article.FirstOrDefault(x => x.Id == id);
            if (rec != null)
            {
                _context.Article.Remove(rec);

            }
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "Success", Messages = messages });

        }


        [HttpPost("api/Article/Insert")]
        public IActionResult InsertArticle([FromBody] ArticleVM toSendData)
        {
            List<string> messages = new List<string>();
            var _currentUserId = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var _currentUser = HttpContext.User.Identity.Name;
                _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            }
            try
            {

                _context.Article.Add(new Article
                {
                    Title = toSendData.Title,
                    Date = toSendData.Date,
                    Description = toSendData.Description,
                    isShow = toSendData.isShow,
                    Keyword = toSendData.Keyword,
                    ExternalLink = toSendData.ExternalLink,
                    AuthorName = toSendData.AuthorName,
                    MenuPathId = toSendData.MenuPathId,
                    ArticleTopicId = toSendData.ArticleTopicId,
                    CreatedUserId = _currentUserId,
                    CreateDate = DateTime.Now.ToString(),
                });
                _context.SaveChanges();
                messages.Add("your data submited successfully.");

            }
            catch (Exception ex)
            {
                messages.Add("there was problem in adding data.");


                throw;
            }


            //var rec = _context.MenuPath.FirstOrDefault(x => x.Id == id);
            //_context.MenuPath.Remove(rec);
            //_context.SaveChanges();
            return Ok(new { Status = "success", Messages = messages });
        }




        [HttpGet("api/Article/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.Article.Include(x => x.Menupath).FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/Article/Edit/")]
        public IActionResult Edit([FromBody] ArticleEditVM article)
        {
            List<string> messages = new List<string>();
            var item = _context.Article.Include(x => x.CreatedUser).Include(x=>x.Menupath).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == article.Id);

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (item != null)
            {
                item.Title = article.Title;
                item.Description = article.Description;
                item.AuthorName = article.AuthorName;
                item.Date = article.Date;
                item.isShow = article.isShow;
                item.Keyword = article.Keyword;
                item.ExternalLink = article.ExternalLink;
                item.MenuPathId = article.MenuPathId;
                item.ArticleTopicId =article.ArticleTopicId;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "success", Data = item, Messages = messages });
        }

    }
}