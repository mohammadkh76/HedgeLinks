using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HedgeLinks.Data;
using HedgeLinks.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    public class JobSeekerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JobSeekerController(ApplicationDbContext context, IHostingEnvironment environment,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            hostingEnvironment = environment;
        }


        [HttpGet]
        [Route("api/JobSeekerRegister/GetAllCountries/")]
        public IActionResult GetAllCounties()
        {
            var Items = _context.Country;
            return Ok(new {Status = "success", Data = Items.ToList()});
        }

        [HttpGet]
        [Route("api/JobSeekerRegister/getStates/{id}")]
        public IActionResult getStates(int id)
        {
            var Items = _context.State.Where(x => x.country_id == id);
            return Ok(new {Status = "success", Data = Items.ToList()});
        }

        [HttpPost]
        [Route("api/JobSeekerRegister/Submit")]
        public async Task<IActionResult> Submit()
        {
            List<string> messages = new List<string>();
            string resumeFile = "";
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string name = String.IsNullOrEmpty(form["FullName"]) ? "" : form["FullName"].ToString();
            string email = String.IsNullOrEmpty(form["Email"]) ? "" : form["Email"].ToString();
            string countryId = String.IsNullOrEmpty(form["CountryId"]) ? "" : form["CountryId"].ToString();
            string stateId = String.IsNullOrEmpty(form["StateId"]) ? "" : form["StateId"].ToString();
            string city = String.IsNullOrEmpty(form["City"]) ? "" : form["City"].ToString();
            string jobTitle = String.IsNullOrEmpty(form["JobTitle"]) ? "" : form["JobTitle"].ToString();
            string password = String.IsNullOrEmpty(form["Password"]) ? "" : form["Password"].ToString();
            string confirmPassword =
                String.IsNullOrEmpty(form["ConfirmPassword"]) ? "" : form["ConfirmPassword"].ToString();

            if (form.Files.Count < 1)
            {
                resumeFile = "/upload/blank-person.png";
            }

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var rootPath = hostingEnvironment.WebRootPath.ToString();
                var serverPath = rootPath + "\\Resume\\" + guid + name + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                resumeFile = "/Resume/" + guid + name + file.FileName;
            }

            if (password.Equals(confirmPassword))
            {
                var _currentUserId = "";

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var _currentUser = HttpContext.User.Identity.Name;
                    _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                }

                ApplicationUser user = new ApplicationUser() {Email = email, UserName = email, EmailConfirmed = true};
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {

                }

                return Ok(new {Status = "success"});
            }
            return Ok(new {Status = "success"});

    }
}
}