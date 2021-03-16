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
    public class StaffAttendenceController : Controller
    {
        private readonly AttendenceRepo _attendenceRepo = null;

        public StaffAttendenceController(AttendenceRepo attendenceRepo)
        {
            _attendenceRepo = attendenceRepo;

        }

        [HttpGet]
        public async Task<IActionResult> AttendenceInfo(DateTime Month)
        {
            int EmpId = 2;
            String monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month.Month);
            if (Month != DateTime.MinValue)
            {
                ViewBag.Month = Month;
                ViewBag.subtitle = "Attendence of " + monthName + " , " + Month.Year;
              var data = await _attendenceRepo.GetAttInfo(EmpId, Month);
                if (data == null)
                {
                    ViewBag.a = DateTime.MinValue;
                    return RedirectToAction(nameof(AttendenceInfo));
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

        [HttpGet]
        public IActionResult AtteRequest(bool isSucceess=false, bool isExist = false)
        {
            ViewBag.IsSuccess = isSucceess;
            ViewBag.IsExist = isExist;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AtteRequest(AttendenceModel attendenceModel)
        {
            attendenceModel.EmpId = 2;
            bool existOne = _attendenceRepo.CheckExist(attendenceModel.EmpId, attendenceModel.Date);
            if (existOne)
            {
                return RedirectToAction(nameof(AtteRequest), new { isExist = true });
            }
            else
            {

                TimeSpan differ = (TimeSpan)(attendenceModel.OutTime - attendenceModel.InTime);
                attendenceModel.TotalHours = differ.TotalHours;
                double breakTime = ((attendenceModel.BHours * 60.0) + attendenceModel.BMinutes) / 60.0;
                attendenceModel.BreakingHours = breakTime;
                attendenceModel.WorkingHours = differ.TotalHours - breakTime;

                int id = await _attendenceRepo.AddRequest(attendenceModel);


                if (id > 0)
                {
                    return RedirectToAction(nameof(AtteRequest), new { isSucceess = true });
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RequestHistory()
        {
            int EmpId = 2;
                var data = await _attendenceRepo.GetRequest(EmpId);
                return View(data);
        }
    }
}
