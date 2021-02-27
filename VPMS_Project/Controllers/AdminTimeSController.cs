using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class AdminTimeSController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly JobRepository _jobRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LeaveRepository _leaveRepository = null;

        public AdminTimeSController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, LeaveRepository leaveRepository, JobRepository jobRepository)
        {
            _empRepository = empRepository;
            _jobRepository = jobRepository;
            _webHostEnvironment = webHostEnvironment;
            _leaveRepository = leaveRepository;
        }
        public async Task<IActionResult> ViewTimeSheetAsync()
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
            return View();
        }
    }
}
