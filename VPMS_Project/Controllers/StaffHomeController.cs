using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class StaffHomeController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LeaveRepository _leaveRepository = null;



        public StaffHomeController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult StaffDash()
        {
            return View();
        }
    }
}
