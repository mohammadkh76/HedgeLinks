using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using System.Web;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HedgeLinks.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/MenuPath")]
    public class MenuPathController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MenuPathController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Produces("application/json")]

        public IActionResult GetMenuPath()
        {
            var q=HttpContext.Request.QueryString;
           var top = HttpContext.Request.Query["$top"].ToString();
           var filter = HttpContext.Request.Query["$filter"].ToString();
           var sort = HttpContext.Request.Query["$orderby"].ToString();
            List<MenuPath> Items = new List<MenuPath>();
            if (_context.MenuPath != null)
            {
                Items = _context.MenuPath.ToList();
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });

        }

    }
}