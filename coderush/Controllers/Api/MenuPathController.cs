using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgeLinks.Data;
using HedgeLinks.Models;
using System.Web;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HedgeLinks.Models.ManageViewModels;
using HedgeLinks.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HedgeLinks.Models.RESTViewModel;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    
    //[ApiController]
    public class MenuPathController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MenuPathController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/User
        [HttpPost]
        [Route("api/MenuPath/GetAll/")]
        public IActionResult PostMenuPath([FromBody]PageVM pages)
        {
            int skip =((pages.Current-1)  * pages.ItemInPage);
            
           var Items = _context.MenuPath.Include(x => x.CreatedUser).Include(x=>x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();

             Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new {Status="success",Data= Items.ToList(), Count=count});
        }
        [HttpGet]
        [Route("api/MenuPath/GetAllMenuPath/")]
        public IActionResult GetAllMenuPath()
        {
            var Items = _context.MenuPath.Include(x => x.CreatedUser).Include(x => x.EditedUser);

            return Ok(new { Status = "Success", Data = Items.ToList()});
        }
        [HttpGet("api/MenuPath/Delete/{id}")]

        public IActionResult DelMenuPath(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.MenuPath.FirstOrDefault(x => x.Id == id);
            if (rec!=null)
            {
                _context.MenuPath.Remove(rec);

            }
            var count = _context.MenuPath.Count() ;
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "Success", Count = count });

        }


        [HttpPost("api/MenuPath/Insert")]
        public IActionResult InsertMenuPath([FromBody] MenuPathVM toSendData)
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
                _context.MenuPath.Add(new MenuPath
                {
                    Name = toSendData.Name,
                    Description = toSendData.Description,
                    PageName = toSendData.PageName,
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
            return Ok(new { Status = "success",Messages=messages });
        }



        [HttpGet("api/Menupath/DescriptionDetail/{id}")]
        public IActionResult ShowDescription(int id) {
            List<string> messages = new List<string>();
            string desc = _context.MenuPath.FirstOrDefault(x => x.Id == id).Description.ToString() ;
            messages.Add("Your Page will show like this...");
            return Ok(new { Status = "success", Data = desc, Messages = messages });
        }

        [HttpGet("api/Menupath/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.MenuPath.FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success",Data=item});
        }
        [HttpPost("api/MenuPath/Edit/")]
        public IActionResult Edit([FromBody] MenuPathEditVM menupath)
        {
            List<string> messages = new List<string>();
            var item = _context.MenuPath.Include(x=>x.CreatedUser).Include(x=>x.EditedUser).FirstOrDefault(x => x.Id == menupath.Id);

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (item!=null)
            {
                item.Name = menupath.Name;
                item.PageName = menupath.PageName;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                item.Description = menupath.Description;
                _context.SaveChanges();
            }
            return Ok(new { Status = "success", Data = item, Messages = messages });
        }

    

    }
}