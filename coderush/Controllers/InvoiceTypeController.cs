﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HedgeLinks.Controllers
{
    [Authorize(Roles = Pages.MainMenu.InvoiceType.RoleName)]
    public class InvoiceTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}