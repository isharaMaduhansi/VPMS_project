using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Models;
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

        [HttpGet]
        public async Task<IActionResult> ViewAttendence(string Search = null)
        {
            var data = await _empRepository.GetSearchEmps(Search);

            if (data == null)
            {
                return RedirectToAction(nameof(ViewAttendence));
            }

            return View(data);
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

        [HttpGet]
        public async Task<IActionResult> SeeAttendenceInfo(int id, DateTime Month)
        {
            String monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month.Month);
            if (Month != DateTime.MinValue)
            {
                ViewBag.subtitle = "Attendence of " + monthName + " , " + Month.Year;
                var data = await _attendenceRepository.GetAttInfo2(id, Month);
                if (data == null)
                {
                    ViewBag.a = DateTime.MinValue;
                    return RedirectToAction(nameof(SeeAttendenceInfo));
                }
                else
                {
                    ViewBag.a = Month;
                    return View(data);
                }


            }
            ViewBag.a = Month;
            return View();
        }


        public async Task<IActionResult> ModifyWorkHours(WorkHourModel workHourModel)
        {
            bool success = await _attendenceRepository.UpdateHour(workHourModel);
            if (success == true)
            {
                return RedirectToAction(nameof(ViewAttendence), new { isUpdate = true });

            }
            return View();
        }
    }
}
