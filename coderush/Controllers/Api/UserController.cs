using HedgeLinks.Data;
using HedgeLinks.Models;
using HedgeLinks.Models.RESTViewModel;
using HedgeLinks.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment hostingEnvironment;
     
        public UserController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            hostingEnvironment = environment;

        }

        // GET: api/User
        [HttpPost]
        [Route("api/User/GetAll/")]
        public IActionResult GetUser([FromBody]PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);
            var Items = _context.UserProfile.Include(x => x.ApplicationUser).OrderByDescending(x => x.Id);
            int count = Items.Count();
            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetByApplicationUserId([FromRoute]string id)
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

        [HttpPost("api/User/Insert")]
        public async Task<IActionResult> Insert()
        {
            List<string> messages = new List<string>();
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string firstName = String.IsNullOrEmpty(form["FirstName"]) ? "" : form["FirstName"].ToString();
            string lastName = String.IsNullOrEmpty(form["LastName"]) ? "" : form["LastName"].ToString();
            string email = String.IsNullOrEmpty(form["Email"]) ? "" : form["Email"].ToString();
            string password = String.IsNullOrEmpty(form["Password"]) ? "" : form["Password"].ToString();
            string confirmPassword = String.IsNullOrEmpty(form["ConfirmPassword"]) ? "" : form["ConfirmPassword"].ToString();
            string profilePic = "";
            if (form.Files[0]==null) {
                 profilePic = "/upload/blank-person.png";

            }
            if (form.Files[0] != null)
            {
                var file = form.Files[0];
                var rootPath = hostingEnvironment.WebRootPath.ToString();
                var serverPath = rootPath + "\\Images\\ProfilePics\\" + guid + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                profilePic = "/Images/ProfilePics/" + guid + file.FileName;


            }
            UserProfile register = new UserProfile();
            if (password.Equals(confirmPassword))
            {
                var _currentUserId = "";

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var _currentUser = HttpContext.User.Identity.Name;
                    _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                }
                ApplicationUser user = new ApplicationUser() { Email = email, UserName = email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    
                    register.CreatedUserId = _currentUserId;
                    register.CreateDate = DateTime.Now.ToString();
                    register.Password = user.PasswordHash;
                    register.FirstName = firstName;
                    register.LastName = lastName;
                    register.Email = email;
                    register.ConfirmPassword = user.PasswordHash;
                    register.ApplicationUserId = user.Id;
                    register.ProfilePicture = profilePic;
                    _context.UserProfile.Add(register);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    messages.Add(result.Errors.First().Description);
                    return BadRequest(new { Status = "Failed", Messages = messages });
                }



            }
            else
            {
                messages.Add("you password didn't Match");
                return BadRequest(new { Status = "Failed", Messages = messages });
            }
            messages.Add("your data Submitted successfully");

            return Ok(new{ Status = "Success", Messages = messages});
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