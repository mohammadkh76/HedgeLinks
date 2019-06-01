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
    public class JobIndustryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JobIndustryController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Route("api/JobIndustry/GetAllJobIndustry/")]
        public IActionResult GetAllJobIndustry()
        {
            var Items = _context.JobIndustries.Include(x => x.CreatedUser).Include(x => x.EditedUser);

            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        // GET: api/User
        [HttpPost]
        [Route("api/JobIndustry/GetAll/")]
        public IActionResult PostJobIndustry([FromBody] PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);

            var Items = _context.JobIndustries.Include(x => x.CreatedUser).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();

            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }
        [HttpGet]
        [Route("api/JobIndustry/GetAllMenuPath/")]
        public IActionResult GetAllMenuPath()
        {
            var Items = _context.JobIndustries.Include(x => x.CreatedUser).Include(x => x.EditedUser);

            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        [HttpGet("api/JobIndustry/Delete/{id}")]

        public IActionResult DelMenuPath(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.JobIndustries.FirstOrDefault(x => x.Id == id);
            if (rec != null)
            {
                _context.JobIndustries.Remove(rec);

            }
            var count = _context.JobIndustries.Count();
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "Success", Count = count });

        }


        [HttpPost("api/JobIndustry/Insert")]
        public IActionResult InsertMenuPath([FromBody] JobIndustryVM toSendData)
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
                _context.JobIndustries.Add(new JobIndustry
                {
                    Title = toSendData.Title,
                    Description = toSendData.Description,
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





        [HttpGet("api/JobIndustry/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.JobIndustries.FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/JobIndustry/Edit/")]
        public IActionResult Edit([FromBody] JobIndustryEditVM menupath)
        {
            List<string> messages = new List<string>();
            var item = _context.JobIndustries.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == menupath.Id);

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (item != null)
            {
                item.Title = menupath.Title;
                item.Description = menupath.Description;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "success", Data = item, Messages = messages });
        }

    }
}