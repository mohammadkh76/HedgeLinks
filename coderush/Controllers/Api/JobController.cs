using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HedgeLinks.Data;
using HedgeLinks.Models;
using HedgeLinks.Models.RESTViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    public class JobController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public JobController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;

            hostingEnvironment = environment;

        }
        [HttpGet]
        [Route("api/Job/GetAllJobCompensation/")]
        public IActionResult GetAllJobCompensation()
        {
            var Items = _context.Job.Select(x => x.Compensation);
            return Ok(new { Status = "success", Data = Items.ToList()});
        }
        // GET: api/User
        [HttpPost]
        [Route("api/Job/GetAll/")]
        public IActionResult GetJob([FromBody] PageVM pages)
        {
            int skip = ((pages.Current - 1) * pages.ItemInPage);

            var Items = _context.Job.Include(x => x.CreatedUser).Include(x=>x.JobType).Include(x=>x.JobIndustry).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();

            Items = Items.Skip(skip).Take(pages.ItemInPage).OrderByDescending(x => x.Id);
            return Ok(new { Status = "success", Data = Items.ToList(), Count = count });
        }

        [HttpPost("api/Job/Insert")]
        public async Task<IActionResult> Insert()
        {
            List<string> messages = new List<string>();
            Job record = new Job();
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string title = String.IsNullOrEmpty(form["Title"]) ? "" : form["Title"].ToString();
            string description = String.IsNullOrEmpty(form["Description"]) ? "" : form["Description"].ToString();
            string shortDescription = String.IsNullOrEmpty(form["ShortDescription"]) ? "" : form["ShortDescription"].ToString();
            string requiredExp= String.IsNullOrEmpty(form["RequiredExp"]) ? "" : form["RequiredExp"].ToString();
            string keyword = String.IsNullOrEmpty(form["Keyword"]) ? "" : form["Keyword"].ToString();
            string address= String.IsNullOrEmpty(form["Address"]) ? "" : form["Address"].ToString();
            string country= String.IsNullOrEmpty(form["Country"]) ? "" : form["Country"].ToString();
            string state= String.IsNullOrEmpty(form["State"]) ? "" : form["State"].ToString();
            string city= String.IsNullOrEmpty(form["City"]) ? "" : form["City"].ToString();
            string compensation= String.IsNullOrEmpty(form["compensation"]) ? "" : form["Compensation"].ToString();
            string datePlaced= String.IsNullOrEmpty(form["DatePlaced"]) ? "" : form["DatePlaced"].ToString();
            string requiredRole= String.IsNullOrEmpty(form["RequiredRole"]) ? "" : form["RequiredRole"].ToString();
            string companyName= String.IsNullOrEmpty(form["CompanyName"]) ? "" : form["CompanyName"].ToString();
            string JobType= String.IsNullOrEmpty(form["JobTypeId"]) ? "" : form["JobTypeId"].ToString();
            string JobIndustry= String.IsNullOrEmpty(form["JobIndustryId"]) ? "" : form["JobIndustryId"].ToString();
            string menuPath= String.IsNullOrEmpty(form["MenuPathId"]) ? "" : form["MenuPathId"].ToString();
            string externalLink= String.IsNullOrEmpty(form["ExternalLink"]) ? "" : form["ExternalLink"].ToString();
            string isEasyApply= String.IsNullOrEmpty(form["IsEasyApply"]) ? "" : form["IsEasyApply"].ToString();
            string isTrend= String.IsNullOrEmpty(form["IsTrend"]) ? "" : form["IsTrend"].ToString();
            string image = "";
            if (form.Files.Count < 1)
            {
                image = "/upload/blank-person.png";

            }
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var rootPath = hostingEnvironment.WebRootPath.ToString();
                var serverPath = rootPath + "\\Images\\Job\\" + guid + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image = "/Images/Job/" + guid + file.FileName;



                var _currentUserId = "";

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var _currentUser = HttpContext.User.Identity.Name;
                    _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                }
                try
                {
                    record.CreatedUserId = _currentUserId;
                    record.CreateDate = DateTime.Now.ToString();
                    record.Title = title;
                    record.Description = description;
                    record.Keyword = keyword;
                    record.ShortDescription = shortDescription;
                    record.RequiredExp = requiredExp;
                    record.RequiredRole = requiredRole;
                    record.Address = address;
                    record.City = city;
                    record.Country = country;
                    record.State = state;
                    record.Compensation = compensation;
                    record.MenuPathId = (menuPath != "0") ? Int32.Parse(menuPath) :(int?)null;
                    record.JobTypeId = Int32.Parse(JobType);
                    record.JobIndustryId = Int32.Parse(JobIndustry);
                    record.CompanyName = companyName;
                    record.DatePlaced = datePlaced;
                    record.ExternalLink = externalLink;
                    record.FilePath = image;
                    record.isEasyApply = (isEasyApply != "") ? bool.Parse(isEasyApply) : false;
                    record.isTrend = (isTrend != "") ? bool.Parse(isTrend) : false;

                    record.FilePath = image;
                    _context.Job.Add(record);

                }
                catch (Exception ex)
                {

                    throw;
                }
                              try
                {
                    await _context.SaveChangesAsync();
                    messages.Add("your data Submitted successfully");

                    return Ok(new { Status = "Success", Messages = messages });

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                messages.Add("You shoud select picture");
                return BadRequest(new { Status = "Failed", Messages = messages });
            }

        }



        [HttpGet("api/Job/Delete/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            List<string> messages = new List<string>();

            var cp = _context.Job.SingleOrDefault(x => x.Id.Equals(id));
            if (cp != null)
            {

                try
                {
                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + cp.FilePath;
                    System.IO.File.Delete(serverPath);
                    _context.Remove(cp);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Remove(cp);
                    await _context.SaveChangesAsync();

                }
                messages.Add("Record Deleted Successfully");
                return Ok(new { Status = "Success", Messages = messages });


            }
            else
            {
                messages.Add("Record Delete Failed");
                return Ok(new { Status = "Failed", Messages = messages });
            }

        }
        [HttpGet("api/Job/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.Job.Include(x=>x.JobIndustry).Include(x=>x.JobType).Include(x=>x.MenuPath).FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/Job/Edit/")]
        public async Task<IActionResult> Edit()
        {
            List<string> messages = new List<string>();
            Job record = new Job();
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string id = String.IsNullOrEmpty(form["Id"]) ? "" : form["Id"].ToString();
            string title = String.IsNullOrEmpty(form["Title"]) ? "" : form["Title"].ToString();
            string description = String.IsNullOrEmpty(form["Description"]) ? "" : form["Description"].ToString();
            string shortDescription = String.IsNullOrEmpty(form["ShortDescription"]) ? "" : form["ShortDescription"].ToString();
            string requiredExp = String.IsNullOrEmpty(form["RequiredExp"]) ? "" : form["RequiredExp"].ToString();
            string keyword = String.IsNullOrEmpty(form["Keyword"]) ? "" : form["Keyword"].ToString();
            string address = String.IsNullOrEmpty(form["Address"]) ? "" : form["Address"].ToString();
            string country = String.IsNullOrEmpty(form["Country"]) ? "" : form["Country"].ToString();
            string state = String.IsNullOrEmpty(form["State"]) ? "" : form["State"].ToString();
            string city = String.IsNullOrEmpty(form["City"]) ? "" : form["City"].ToString();
            string compensation = String.IsNullOrEmpty(form["compensation"]) ? "" : form["Compensation"].ToString();
            string datePlaced = String.IsNullOrEmpty(form["DatePlaced"]) ? "" : form["DatePlaced"].ToString();
            string requiredRole = String.IsNullOrEmpty(form["RequiredRole"]) ? "" : form["RequiredRole"].ToString();
            string companyName = String.IsNullOrEmpty(form["CompanyName"]) ? "" : form["CompanyName"].ToString();
            string JobType = String.IsNullOrEmpty(form["JobTypeId"]) ? "" : form["JobTypeId"].ToString();
            string JobIndustry = String.IsNullOrEmpty(form["JobIndustryId"]) ? "" : form["JobIndustryId"].ToString();
            string menuPath = String.IsNullOrEmpty(form["MenuPathId"]) ? "" : form["MenuPathId"].ToString();
            string externalLink = String.IsNullOrEmpty(form["ExternalLink"]) ? "" : form["ExternalLink"].ToString();
            string isEasyApply = String.IsNullOrEmpty(form["IsEasyApply"]) ? "" : form["IsEasyApply"].ToString();
            string isTrend = String.IsNullOrEmpty(form["IsTrend"]) ? "" : form["IsTrend"].ToString();
            string oldImage = String.IsNullOrEmpty(form["OldImage"]) ? "" : form["OldImage"].ToString();
            string image = "";
            var item = _context.Job.Include(x => x.CreatedUser).Include(x => x.EditedUser).Include(x => x.JobIndustry).Include(x => x.JobType).Include(x => x.MenuPath).FirstOrDefault(x => x.Id == Int32.Parse(id));

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (form.Files.Count > 0)
            {

                try
                {
                    var file = form.Files[0];

                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + oldImage;
                    System.IO.File.Delete(serverPath);

                    var serverPath2 = rootPath + "/Images/Job/" + guid + file.FileName;
                    using (var stream = new FileStream(serverPath2, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    item.FilePath = "/Images/Job/" + guid + file.FileName;
                }
                catch (Exception ex)
                {
                    var file = form.Files[0];
                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + "/Images/Job/" + guid + file.FileName;
                    using (var stream = new FileStream(serverPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    item.FilePath = "/Images/Job/" + guid + file.FileName;
                }
            }
            else
            {
                item.FilePath = oldImage;
            }
            if (item != null)
            {
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                item.Title = title;
                item.Description = description;
                item.Keyword = keyword;
                item.ShortDescription = shortDescription;
                item.RequiredExp = requiredExp;
                item.RequiredRole = requiredRole;
                item.Address = address;
                item.City = city;
                item.Country = country;
                item.State = state;
                item.Compensation = compensation;
                item.MenuPathId = (menuPath != "0") ? Int32.Parse(menuPath) : (int?)null;
                item.JobTypeId = Int32.Parse(JobType);
                item.JobIndustryId = Int32.Parse(JobIndustry);
                item.CompanyName = companyName;
                item.DatePlaced = datePlaced;
                item.ExternalLink = externalLink;
                item.FilePath = image;
                item.isEasyApply = (isEasyApply != "") ? bool.Parse(isEasyApply) : false;
                item.isTrend = (isTrend != "") ? bool.Parse(isTrend) : false;

                item.FilePath = image;
                _context.SaveChanges();
            }
            return Ok(new { Status = "Success", Data = item, Messages = messages });
        }

    }
}