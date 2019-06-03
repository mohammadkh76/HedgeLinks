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

    public class ThirdSectionController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public ThirdSectionController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;

            hostingEnvironment = environment;

        }
        [HttpGet]
        [Route("api/ThirdSection/GetAllThirdSection/")]
        public IActionResult GetAllThirdSection()
        {
            var Items = _context.ThirdSection.First() ;
            return Ok(new { Status = "success", Data = Items });
        }
        // GET: api/User
        [HttpGet]
        [Route("api/ThirdSection/GetAll/")]
        public IActionResult GetThirdSection()
        {
            var Items = _context.ThirdSection.Include(x => x.CreatedUser).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();
            return Ok(new { Status = "success", Data = Items.ToList() });
        }

        [HttpPost("api/ThirdSection/Insert")]
        public async Task<IActionResult> Insert()
        {
            List<string> messages = new List<string>();
            ThirdSection record = new ThirdSection();
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string title = String.IsNullOrEmpty(form["Title"]) ? "" : form["Title"].ToString();
            string subtitle = String.IsNullOrEmpty(form["Subtitle"]) ? "" : form["Subtitle"].ToString();
            string keyword = String.IsNullOrEmpty(form["Keyword"]) ? "" : form["Keyword"].ToString();
            string image = "";
            if (form.Files.Count < 1)
            {
                image = "/upload/blank-person.png";

            }
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var rootPath = hostingEnvironment.WebRootPath.ToString();
                var serverPath = rootPath + "\\Images\\ThirdSection\\" + guid + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image = "/Images/ThirdSection/" + guid + file.FileName;



                var _currentUserId = "";

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var _currentUser = HttpContext.User.Identity.Name;
                    _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                }

                record.CreatedUserId = _currentUserId;
                record.CreateDate = DateTime.Now.ToString();
                record.Title = title;
                record.Subtitle = subtitle;
                record.Keyword = keyword;
                record.FilePath = image;
                _context.ThirdSection.Add(record);
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



        [HttpGet("api/ThirdSection/Delete/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            List<string> messages = new List<string>();

            var thirdSection = _context.ThirdSection.SingleOrDefault(x => x.Id.Equals(id));
            if (thirdSection != null)
            {

                try
                {
                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + thirdSection.FilePath;
                    System.IO.File.Delete(serverPath);
                    _context.Remove(thirdSection);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Remove(thirdSection);
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
        [HttpGet("api/ThirdSection/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.ThirdSection.FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/ThirdSection/Edit/")]
        public async Task<IActionResult> Edit()
       {
            var form = Request.Form;
            string id = String.IsNullOrEmpty(form["Id"]) ? "" : form["Id"].ToString();
            string title = String.IsNullOrEmpty(form["Title"]) ? "" : form["Title"].ToString();
            string subtitle = String.IsNullOrEmpty(form["Subtitle"]) ? "" : form["Subtitle"].ToString();
            string keyword = String.IsNullOrEmpty(form["Keyword"]) ? "" : form["Keyword"].ToString();
            string imagePath = String.IsNullOrEmpty(form["ImagePath"]) ? "" : form["ImagePath"].ToString();
            string oldImagePath = String.IsNullOrEmpty(form["OldImagePath"]) ? "" : form["OldImagePath"].ToString();
            List<string> messages = new List<string>();
            var item = _context.ThirdSection.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == Int32.Parse(id) );

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (form.Files.Count > 0)
            {
                var rnd = new Random();
                var guid = rnd.Next(999);
                try
                {
                    var file = form.Files[0];

                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + oldImagePath;

                    System.IO.File.Delete(serverPath);
                    var serverPath2 = rootPath + "\\Images\\ThirdSection\\" + guid + file.FileName;

                    using (var stream = new FileStream(serverPath2, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    item.FilePath = "/Images/ThirdSection/" + guid + file.FileName;
                }
                catch (Exception ex)
                {
                    var file = form.Files[0];
                    var rootPath = hostingEnvironment.WebRootPath.ToString();

                    var serverPath2 = rootPath + "\\Images\\ThirdSection\\" + guid + file.FileName;

                    using (var stream = new FileStream(serverPath2, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    item.FilePath = "/Images/ThirdSection/" + guid + file.FileName;
                }
            }
            else
            {
                item.FilePath = imagePath;
            }
            if (item != null)
            {
                item.Title = title;
                item.Subtitle = subtitle;
                item.Keyword = keyword;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "Success", Data = item, Messages = messages });
        }


    }
}