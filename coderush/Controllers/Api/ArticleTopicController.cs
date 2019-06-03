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
    public class ArticleTopicController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ArticleTopicController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Route("api/ArticleTopic/GetAllArticleTopic/")]
        public IActionResult GetAllArticleTopic()
        {

                var Items = _context.ArticleTopic;



            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        [HttpGet]
        [Route("api/ArticleTopic/GetAllShowedArticleTopic/")]
        public IActionResult GetAllShowedArticleTopic()
        {

            var Items = _context.ArticleTopic.Where(x=>x.IsShow==true);



            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        // GET: api/User
        [HttpPost]
        [Route("api/ArticleTopic/GetAll/")]
        public IActionResult PostMenuPath([FromBody] PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);

            var Items = _context.ArticleTopic.Include(x => x.CreatedUser).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();

            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }
        [HttpGet]
        [Route("api/ArticleTopic/GetAllMenuPath/")]
        public IActionResult GetAllMenuPath()
        {
            var Items = _context.ArticleTopic.Include(x => x.CreatedUser).Include(x => x.EditedUser);

            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        [HttpGet("api/ArticleTopic/Delete/{id}")]

        public IActionResult DelMenuPath(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.ArticleTopic.FirstOrDefault(x => x.Id == id);
            if (rec != null)
            {
                _context.ArticleTopic.Remove(rec);

            }
            var count = _context.ArticleTopic.Count();
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "Success", Count = count });

        }


        [HttpPost("api/ArticleTopic/Insert")]
        public IActionResult InsertMenuPath([FromBody] ArticleTopicVM toSendData)
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
                _context.ArticleTopic.Add(new ArticleTopic
                {
                    Title = toSendData.Title,
                    Description = toSendData.Description,
                    IsShow = toSendData.IsShow,
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



       

        [HttpGet("api/ArticleTopic/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.ArticleTopic.FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/ArticleTopic/Edit/")]
        public IActionResult Edit([FromBody] ArticleTopicEditVM menupath)
        {
            List<string> messages = new List<string>();
            var item = _context.ArticleTopic.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == menupath.Id);

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (item != null)
            {
                item.Title = menupath.Title;
                item.Description = menupath.Description;
                item.IsShow = menupath.IsShow;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "success", Data = item, Messages = messages });
        }

    }
}