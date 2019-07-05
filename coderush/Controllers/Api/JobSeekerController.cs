using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HedgeLinks.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    public class JobSeekerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public JobSeekerController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
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
            string JobTitle = String.IsNullOrEmpty(form["JobTitle"]) ? "" : form["JobTitle"].ToString();
            string Password = String.IsNullOrEmpty(form["Password"]) ? "" : form["Password"].ToString();
            if (form.Files.Count < 1)
            {
                resumeFile = "/upload/blank-person.png";
            }

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var rootPath = hostingEnvironment.WebRootPath.ToString();
                var serverPath = rootPath + "\\Resume\\" + guid + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                resumeFile = "/Resume/" + guid + file.FileName;
            }

            return Ok(new {Status = "success"});
        }
    }
}
