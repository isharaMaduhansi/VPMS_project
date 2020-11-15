using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VPMS_Project.Controllers
{
    public class EmployeeHomeController : Controller
    {
        public IActionResult EmpIndex()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Portal()
        {
            return View();
        }
    }
}
