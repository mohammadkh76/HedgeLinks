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
    public class CommercialTipsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public CommercialTipsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;

            hostingEnvironment = environment;

        }
        [HttpGet]
        [Route("api/CommercialTips/GetAllCommercialTips/")]
        public IActionResult GetAllTopImage()
        {
            var Items = _context.ComercialTips.Include(x => x.CreatedUser).Include(x => x.EditedUser);

            return Ok(new { Status = "Success", Data = Items.ToList() });
        }
        // GET: api/User
        [HttpGet]
        [Route("api/CommercialTips/GetAll/")]
        public IActionResult GetTopImage()
        {
            var Items = _context.ComercialTips.Include(x => x.CreatedUser).Include(x => x.EditedUser).OrderByDescending(x => x.Id);
            int count = Items.Count();
            return Ok(new { Status = "success", Data = Items.ToList() });
        }

        [HttpPost("api/CommercialTips/Insert")]
        public async Task<IActionResult> Insert()
        {
            List<string> messages = new List<string>();
            ComercialTips record = new ComercialTips();
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
                var serverPath = rootPath + "\\Images\\CommercialTips\\" + guid + file.FileName;
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image = "/Images/CommercialTips/" + guid + file.FileName;



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
                _context.ComercialTips.Add(record);
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



        [HttpGet("api/CommercialTips/Delete/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            List<string> messages = new List<string>();

            var cp = _context.ComercialTips.SingleOrDefault(x => x.Id.Equals(id));
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
        [HttpGet("api/CommercialTips/GetEditData/{id}")]
        public IActionResult GetEditData(int id)
        {
            List<string> messages = new List<string>();
            var item = _context.ComercialTips.FirstOrDefault(x => x.Id == id);

            return Ok(new { Status = "success", Data = item });
        }
        [HttpPost("api/CommercialTips/Edit/")]
        public async Task<IActionResult> Edit()
        {
            List<string> messages = new List<string>();
            ComercialTips record = new ComercialTips();
            var form = Request.Form;
            var rnd = new Random();
            var guid = rnd.Next(999);
            string id = String.IsNullOrEmpty(form["Id"]) ? "" : form["Id"].ToString();
            string filePath = String.IsNullOrEmpty(form["FilePath"]) ? "" : form["FilePath"].ToString();
            string title = String.IsNullOrEmpty(form["Title"]) ? "" : form["Title"].ToString();
            string subtitle = String.IsNullOrEmpty(form["Subtitle"]) ? "" : form["Subtitle"].ToString();
            string keyword = String.IsNullOrEmpty(form["Keyword"]) ? "" : form["Keyword"].ToString();
            string image = "";
            var item = _context.ComercialTips.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == Int32.Parse(id));

            var _currentUser = HttpContext.User.Identity.Name;
            var _currentUserId = _context.ApplicationUser.FirstOrDefault(x => x.UserName == _currentUser).Id;
            if (form.Files.Count > 0)
            {
                
                try
                {
                    var file = form.Files[0];

                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + filePath;
                    System.IO.File.Delete(serverPath);
                  
                    var serverPath2 = rootPath + "/Images/CommercialTips/" + guid + file.FileName;
                    using (var stream = new FileStream(serverPath2, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    item.FilePath = "/Images/CommercialTips/" + guid + file.FileName;
                }
                catch (Exception ex)
                {
                    var file = form.Files[0];
                    var rootPath = hostingEnvironment.WebRootPath.ToString();
                    var serverPath = rootPath + "/Images/CommercialTips/" + guid + file.FileName;
                    using (var stream = new FileStream(serverPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    item.FilePath = "/Images/CommercialTips/" + guid + file.FileName;
                }
            }
            else
            {
                item.FilePath =filePath;
            }
            if (item != null)
            {
                item.Subtitle = subtitle;
                item.Title = title ;
                item.Keyword = keyword;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "Success", Data = item, Messages = messages });
        }


    }
}