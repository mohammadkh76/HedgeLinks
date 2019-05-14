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
            List<MenuPath> Items = new List<MenuPath>();
            Items = _context.MenuPath.Include(x => x.CreatedUser).ToList();
            int count = Items.Count();

            Items = _context.MenuPath.Include(x => x.CreatedUser).Skip(skip).Take(pages.ItemInPage).ToList();
            return Ok(new {Status="success",Data=Items,Count=count});
        }
        [HttpGet("api/MenuPath/Delete/{id}")]

        public IActionResult DelMenuPath(int id)
        {
            List<string> messages = new List<string>();

            var rec = _context.MenuPath.FirstOrDefault(x => x.Id == id);
            _context.MenuPath.Remove(rec);
            var count = _context.MenuPath.Count() ;
            _context.SaveChanges();
            messages.Add("your data deleted successfully.");

            return Ok(new { Status = "success", Count = count });

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
                    ApplicationUserId = _currentUserId,
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

        [HttpGet("[action]/{id}")]
        public IActionResult GetByApplicationMenuPathId([FromRoute]string id)
        {
            UserProfile userProfile = _context.UserProfile.SingleOrDefault(x => x.ApplicationUserId.Equals(id));
            List<UserProfile> Items = new List<UserProfile>();
            if (userProfile != null)
            {
                Items.Add(userProfile);
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<UserProfile> payload)
        {
            UserProfile register = payload.value;
            if (register.Password.Equals(register.ConfirmPassword))
            {
                ApplicationUser user = new ApplicationUser() { Email = register.Email, UserName = register.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    register.Password = user.PasswordHash;
                    register.ConfirmPassword = user.PasswordHash;
                    register.ApplicationUserId = user.Id;
                    _context.UserProfile.Add(register);
                    await _context.SaveChangesAsync();
                }

            }
            return Ok(register);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            _context.UserProfile.Update(profile);
            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody]CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            if (profile.Password.Equals(profile.ConfirmPassword))
            {
                var user = await _userManager.FindByIdAsync(profile.ApplicationUserId);
                var result = await _userManager.ChangePasswordAsync(user, profile.OldPassword, profile.Password);
            }
            profile = _context.UserProfile.SingleOrDefault(x => x.ApplicationUserId.Equals(profile.ApplicationUserId));
            return Ok(profile);
        }

        [HttpPost("[action]")]
        public IActionResult ChangeRole([FromBody]CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            return Ok(profile);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody]CrudViewModel<UserProfile> payload)
        {
            var userProfile = _context.UserProfile.SingleOrDefault(x => x.UserProfileId.Equals((int)payload.key));
            if (userProfile != null)
            {
                var user = _context.Users.Where(x => x.Id.Equals(userProfile.ApplicationUserId)).FirstOrDefault();
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _context.Remove(userProfile);
                    await _context.SaveChangesAsync();
                }

            }

            return Ok();

        }


    }
}