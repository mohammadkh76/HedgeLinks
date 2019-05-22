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
    public class SubMenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SubMenuController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/User
        [HttpPost]
        [Route("api/Submenu/GetAll/")]
        public IActionResult PostSubmenu([FromBody] PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);

            var Items = _context.Submenu.Include(x => x.CreatedUser).Include(x => x.EditedUser).Include(x => x.Menubar).Include(x => x.MenuPath).OrderByDescending(x => x.Id);
            int count = Items.Count();

            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }
        [HttpGet("api/Submenu/Delete/{id}")]

        public IActionResult DelSubmenu(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.Submenu.Include(x => x.Menubar).Include(x => x.MenuPath).FirstOrDefault(x => x.Id == id);
            if (rec != null)
            {
                _context.Submenu.Remove(rec);

            }
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "Success", Messages = messages });

        }


        [HttpPost("api/Submenu/Insert")]
        public IActionResult InsertSubmenu([FromBody] SubmenuVM toSendData)
        {
            List<string> messages = new List<string>();
            if (toSendData != null)
            {

                if (toSendData.SelectedMenu == 0)
                {
                    messages.Add("you should select Menu");
                    return BadRequest(new { Status = "Failed", Messages = messages });
                }
                var _currentUserId = "";
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var _currentUser = HttpContext.User.Identity.Name;
                    _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                }
                try
                {

                    _context.Submenu.Add(new SubMenu
                    {
                        Name = toSendData.Name,
                        Path = toSendData.Path,
                        MenuPathId = toSendData.SelectedPage,
                        MenubarId = toSendData.SelectedMenu,
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

            }
            return Ok(new { Status = "success", Messages = messages });
        }




        [HttpGet("api/Submenu/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.Submenu.Include(x => x.MenuPath).Include(x => x.Menubar).FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/Submenu/Edit/")]
        public IActionResult Edit([FromBody] SubmenuEditVM submenu)
        {
            List<string> messages = new List<string>();


            if (submenu != null)
            {


                if (submenu.SelectedMenu == 0)
                {

                    messages.Add("you should select Menu");
                    return BadRequest(new { Status = "Failed", Messages = messages });
                }
                var item = _context.Submenu.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == submenu.Id);

                var _currentUser = HttpContext.User.Identity.Name;
                var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                if (item != null)
                {
                    item.Name = submenu.Name;
                    item.Path = submenu.Path;
                    item.MenuPathId = submenu.SelectedPage;
                    item.MenubarId = submenu.SelectedMenu;
                    item.EditUserId = _currentUserId;
                    item.EditDate = DateTime.Now.ToString();
                    _context.SaveChanges();
                }
            }
            return Ok(new { Status = "success", Messages = messages });


        }
    }
}