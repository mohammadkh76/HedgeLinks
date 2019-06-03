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
    public class TopImageController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public TopImageController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;

            hostingEnvironment = environment;

        }
        [HttpGet]
        [Route("api/TopImage/GetAllTopImage/")]
        public IActionResult GetAllTopImage()
        {
            TopImage Items = new TopImage();

            if (_context.TopImage.Count()>0)
            {
                 Items = _context.TopImage.First();

            }

            return Ok(new { Status = "Success", Data = Items });
        }
        // GET: api/User
        [HttpGet]
        [Route("api/TopImage/GetAll/")]
        public IActionResult GetTopImage()
        {
            var Items = _context.TopImage.Include(x => x.CreatedUser).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();
            return Ok(new { Status = "success", Data = Items.ToList() });
        }

        [HttpPost("api/TopImage/Insert")]
        public async Task<IActionResult> Insert()
        {
            List<string> messages = new List<string>();
            TopImage record = new TopImage();
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string title = String.IsNullOrEmpty(form["ImageTitle"]) ? "" : form["ImageTitle"].ToString();
            string subtitle = String.IsNullOrEmpty(form["ImageSubtitle"]) ? "" : form["ImageSubtitle"].ToString();
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
                var serverPath = rootPath + "\\Images\\TopImage\\" + guid + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image = "/Images/TopImage/" + guid + file.FileName;



                var _currentUserId = "";

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var _currentUser = HttpContext.User.Identity.Name;
                    _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
                }

                record.CreatedUserId = _currentUserId;
                record.CreateDate = DateTime.Now.ToString();
                record.ImageTitle = title;
                record.ImageSubtitle = subtitle;
                record.Keyword = keyword;
                record.FilePath = image;
                _context.TopImage.Add(record);
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



        [HttpGet("api/TopImage/Delete/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            List<string> messages = new List<string>();

            var topImage = _context.TopImage.SingleOrDefault(x => x.Id.Equals(id));
            if (topImage != null)
            {

                try
                {
                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + topImage.FilePath;
                    System.IO.File.Delete(serverPath);
                    _context.Remove(topImage);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Remove(topImage);
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
        [HttpGet("api/TopImage/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.TopImage.FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/TopImage/Edit/")]
        public async Task<IActionResult> Edit()
        {
            var form = Request.Form;
            string title = String.IsNullOrEmpty(form["ImageTitle"]) ? "" : form["ImageTitle"].ToString();
            string id = String.IsNullOrEmpty(form["Id"]) ? "" : form["Id"].ToString();
            string subtitle = String.IsNullOrEmpty(form["ImageSubtitle"]) ? "" : form["ImageSubtitle"].ToString();
            string keyword = String.IsNullOrEmpty(form["Keyword"]) ? "" : form["Keyword"].ToString();
            string imagePath = String.IsNullOrEmpty(form["FilePath"]) ? "" : form["FilePath"].ToString();
            string oldImage = String.IsNullOrEmpty(form["OldImage"]) ? "" : form["OldImage"].ToString();
            string image = "";
            List<string> messages = new List<string>();
            var item = _context.TopImage.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == Int32.Parse(id));

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
                    var serverPath = rootPath + oldImage;
                   
                    System.IO.File.Delete(serverPath);
                    item.FilePath = "/Images/TopImage/" + guid + file.FileName;
                    var serverPath2 = rootPath + "\\Images\\TopImage\\" + guid + file.FileName;
                    using (var stream = new FileStream(serverPath2, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    image = "/Images/TopImage/" + guid + file.FileName;
                    item.FilePath = image;


                }
                catch (Exception ex)
                {
                    var file = form.Files[0];
                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath2 = rootPath + "\\Images\\TopImage\\" + guid + file.FileName;
                    using (var stream = new FileStream(serverPath2, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    image = "/Images/TopImage/" + guid + file.FileName;

                item.FilePath = image;

                }
            }
            else
            {
                item.FilePath = oldImage;
            }
            if (item != null)
            {
                item.ImageSubtitle = subtitle;
                item.ImageTitle =title;
                item.Keyword = keyword;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "Success", Data = item, Messages = messages });
        }



    }
}