using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HedgeLinks.Controllers
{
    public class MenuPagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}