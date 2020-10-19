using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VPMS_Project.Controllers
{
    public class AdminAttendenceController : Controller
    {
        public IActionResult ViewAttendence()
        {
            return View();
        }
    }
}
