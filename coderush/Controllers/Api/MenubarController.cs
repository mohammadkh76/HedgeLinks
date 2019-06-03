using HedgeLinks.Data;
using HedgeLinks.Models;
using HedgeLinks.Models.RESTViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    public class MenubarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MenubarController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/User
        [HttpPost]
        [Route("api/Menubar/GetAll/")]
        public IActionResult PostMenubar([FromBody] PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);

            var Items = _context.Menubar.Include(x => x.CreatedUser).Include(x => x.EditedUser).Include(x=>x.MenuPath).OrderByDescending(x => x.Id);
            int count = Items.Count();

            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }
        [HttpGet]
        [Route("api/Menubar/GetAllMenubar/")]
        public IActionResult GetAllMenuPath()
        {
            var Items = _context.Menubar.Include(x => x.SubMenus);
            var path = "";
            foreach (var item in Items)
            {

                if (item.MenuPathId != null)
                {
                    MenuPath menuPath = _context.MenuPath.Find(item.MenuPathId);
                    path = "/MenuPages/Page?PageName=" + menuPath.PageName;
                }
                else
                {
                    path = item.Path;
                }

                item.Path = path;
            }



            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        [HttpGet("api/Menubar/Delete/{id}")]

        public IActionResult DelMenubar(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.Menubar.FirstOrDefault(x => x.Id == id);
            if (rec != null)
            {
                _context.Menubar.Remove(rec);

            }
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "Success", Messages = messages });

        }


        [HttpPost("api/Menubar/Insert")]
        public IActionResult InsertMenubar([FromBody] MenubarVM toSendData)
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

                _context.Menubar.Add(new Menubar
                {
                    Name = toSendData.Name,
                    Path = toSendData.Path,
                    MenuPathId = toSendData.SelectedPage == 0 ? (int?)null :toSendData.SelectedPage,
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




        [HttpGet("api/Menubar/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.Menubar.Include(x => x.MenuPath).FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/Menubar/Edit/")]
        public IActionResult Edit([FromBody] MenubarEditVM menubar)
        {
            List<string> messages = new List<string>();
            var item = _context.Menubar.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == menubar.Id);

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (item != null)
            {
                item.Name = menubar.Name;
                item.Path = menubar.Path;
                item.MenuPathId = menubar.SelectedPage == 0 ? (int?)null : menubar.SelectedPage;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "success", Data = item, Messages = messages });
        }


    }
}