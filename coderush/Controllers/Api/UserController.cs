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
        [HttpGet("api/User/GetUser/{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            UserProfile userProfile = _context.UserProfile.SingleOrDefault(x => x.Id.Equals(id));
            List<UserProfile> Items = new List<UserProfile>();
            if (userProfile != null)
            {
                Items.Add(userProfile);
            }
            return Ok(new { Status = "Success", Data = Items[0] });
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
            if (form.Files.Count<1)
            {
                profilePic = "/upload/blank-person.png";

            }
            if (form.Files.Count>0)
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
                    try
                    {
                        await _context.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
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

            return Ok(new { Status = "Success", Messages = messages });
        }
        [HttpPost("api/User/ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM Data)
        {
            UserProfile profile = new UserProfile();
            List<string> messages = new List<string>();
            if (Data.Password.Equals(Data.ConfirmPassword))
            {
                var selectedUserProfile = _context.UserProfile.FirstOrDefault(x => x.Id == Int32.Parse(Data.SelectedId));
                var user = await _userManager.FindByIdAsync(selectedUserProfile.ApplicationUserId);
                var result = await _userManager.ChangePasswordAsync(user, Data.OldPassword, Data.Password);
                if (result.Errors.Count() > 0)
                {
                    messages.Add(result.Errors.First().Description.ToString());
                    return BadRequest(new { Status = "Failed", Messages = messages });

                }
                else
                {
                    profile = _context.UserProfile.SingleOrDefault(x => x.ApplicationUserId.Equals(Data.SelectedId));


                }

            }
            else
            {
                messages.Add("your new password didn't match");
                return BadRequest(new { Status = "Failed", Messages = messages });

            }
            messages.Add("password changed successfully");
            return Ok(new { Status = "Success", Messages = messages });

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



        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            _context.UserProfile.Update(profile);
            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpGet("api/User/Delete/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            List<string> messages = new List<string>();

            var userProfile = _context.UserProfile.SingleOrDefault(x => x.Id.Equals(id));
            if (userProfile != null)
            {
                var user = _context.ApplicationUser.SingleOrDefault(x => x.Id == userProfile.ApplicationUserId);

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    try
                    {
                        var rootPath = hostingEnvironment.WebRootPath.ToString();
                        var serverPath = rootPath + userProfile.ProfilePicture;
                        System.IO.File.Delete(serverPath);
                        _context.Remove(userProfile);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _context.Remove(userProfile);
                        await _context.SaveChangesAsync();

                    }

                }

            }
            messages.Add("Record Deleted Successfully"); 
            return Ok(new { Status = "Success", Messages = messages });

        }



        [HttpPost("[action]")]
        public IActionResult ChangeRole([FromBody]CrudViewModel<UserProfile> payload)
        {
            UserProfile profile = payload.value;
            return Ok(profile);
        }


    }
}