using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class AdminAttendenceController : Controller

    {
        private readonly IEmpRepository _empRepository = null;
        private readonly JobRepository _jobRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AttendenceRepo _attendenceRepository = null;

        public AdminAttendenceController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, AttendenceRepo attendenceRepo, JobRepository jobRepository)
        {
            _empRepository = empRepository;
            _jobRepository = jobRepository;
            _webHostEnvironment = webHostEnvironment;
            _attendenceRepository = attendenceRepo;
        }

        public IActionResult ViewAttendence()
        {
            return View();
        }

        public IActionResult RequestHistory()
        {
            return View();
        }

        public async Task<IActionResult> AttApprove(bool isApprove = false, bool isNotApprove = false)
        {
            ViewBag.IsApprove = isApprove;
            ViewBag.IsNotApprove = isNotApprove;
            var data = await _attendenceRepository.GetAttendenceApprove();
            return View(data);
        }

        public async Task<IActionResult> RequestApprove(int id)
        {
            String name = "Ishuwara Adithya";
            bool success = await _attendenceRepository.ApproveAttendence(id, name);
            if (success == true)
            {
                return RedirectToAction(nameof(AttApprove), new { isApprove = true });
            }
            return View();
        }

        public async Task<IActionResult> RequestNotApprove(int id)
        {
            String name = "Ishuwara Adithya";
            bool success = await _attendenceRepository.NotApproveAttendence(id, name);
            if (success == true)
            {
                return RedirectToAction(nameof(AttApprove), new { isNotApprove = true });

            }
            return View();
        }
    }
}
