﻿using System;
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



        [HttpGet("api/User/Delete/{id}")]
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
        public IActionResult Edit([FromBody] TopImageEditVM topimage)
        {
            var form = Request.Form;
            List<string> messages = new List<string>();
            var item = _context.TopImage.Include(x => x.CreatedUser).Include(x => x.EditedUser).FirstOrDefault(x => x.Id == topimage.Id);

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
                    var serverPath = rootPath + topimage.FilePath;
                   
                    System.IO.File.Delete(serverPath);
                    item.FilePath = "/Images/TopImage/" + guid + file.FileName;
                }
                catch (Exception ex)
                {
                    var file = form.Files[0];

                    item.FilePath = "/Images/TopImage/" + guid + file.FileName;
                }
            }
            else
            {
                item.FilePath = topimage.FilePath;
            }
            if (item != null)
            {
                item.ImageSubtitle = topimage.ImageSubtitle;
                item.ImageTitle = topimage.ImageTitle;
                item.Keyword = topimage.Keyword;
                item.EditUserId = _currentUserId;
                item.EditDate = DateTime.Now.ToString();
                _context.SaveChanges();
            }
            return Ok(new { Status = "success", Data = item, Messages = messages });
        }



    }
}