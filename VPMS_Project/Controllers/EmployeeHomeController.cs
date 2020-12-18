using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Data;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class EmployeeHomeController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AttendenceRepo _attendenceRepo = null;
        private readonly EmpStoreContext _context = null;


        public EmployeeHomeController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, AttendenceRepo attendenceRepo, EmpStoreContext context)
        {
            _attendenceRepo = attendenceRepo;
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;

        }

        public IActionResult EmpIndex()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Portal(bool isExist = false, bool isOutExist = false,bool isFail = false)
        {
            int EmpId = 110;
            bool result =  _attendenceRepo.CheckExist(EmpId);

            if (result == true) 
            {
                var data = await _attendenceRepo.GetTime(EmpId); 
                 bool check = _attendenceRepo.CheckOut(EmpId);
                Double dc2 = Math.Round((Double)data.BreakingHours, 2);
                if (check == true)
                {
                    TimeSpan differ = (TimeSpan)(DateTime.Now - data.InTime);
                    Double dc = Math.Round((Double)differ.TotalHours, 2);
                    Double dc3 = Math.Round((Double)(dc - dc2), 2);

                    var timeSpan = TimeSpan.FromHours(dc3);
                    int hh = timeSpan.Hours;
                    int mm = timeSpan.Minutes;
                    ViewBag.Work = hh+"h "+mm+"m";
                    ViewBag.Out = "Not been enterd";
                }
                else
                {
                    TimeSpan differ = (TimeSpan)(data.OutTime - data.InTime);
                    Double dc = Math.Round((Double)differ.TotalHours, 2);
                    Double dc3 = Math.Round((Double)(dc - dc2), 2);
                    var timeSpan = TimeSpan.FromHours(dc3);
                    int hh = timeSpan.Hours;
                    int mm = timeSpan.Minutes;
                    ViewBag.Work = hh + "h " + mm + "m";
                    ViewBag.Out = data.OutTime.ToString("hh:mm tt");
                }
                   
                ViewBag.Track = data.TrackId; 
                Double brk = Math.Round((Double)data.BreakingHours, 2);
                var timeSpan1 = TimeSpan.FromHours(brk);
                int hh1 = timeSpan1.Hours;
                int mm1 = timeSpan1.Minutes;
                ViewBag.Break = hh1 + " h " + mm1 + " minutes";
                ViewBag.In = data.InTime.ToString("hh:mm tt");
                ViewBag.IsExist = isExist;
                ViewBag.IsFail = false;
                ViewBag.IsOutExist = isOutExist;
                return View(data);
            }
            else 
            {
                ViewBag.Track = 0;
                ViewBag.Work=0;
                ViewBag.Break = 0;
                ViewBag.In = "Not been enterd";
                ViewBag.Out = "Not been enterd";
                ViewBag.IsFail = isFail;
                //ViewBag.IsOutExist = false;
                return View();
            }
            
        }

        public async Task<IActionResult> InTime(TimeTrackerModel timeTrackerModel)
        {
            timeTrackerModel.EmpId = 110;
            bool existOne = _attendenceRepo.CheckIn(timeTrackerModel.EmpId);
            if (existOne)
            {
                return RedirectToAction(nameof(Portal), new { isExist = true });
            }
            else
            {
                int id = await _attendenceRepo.InTimeMark(timeTrackerModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(Portal));
                }
            }
            return View();
        }

        public async Task<IActionResult> OutTime(int id)
        {

            if (id == 0)
            {
                return RedirectToAction(nameof(Portal), new { isFail = true });
            }
            else
            {
                TimeTrackerModel timeTrackerModel = new TimeTrackerModel();
                var track = await _context.TimeTracker.FindAsync(id);
                TimeSpan differ = (TimeSpan)(DateTime.Now - track.InTime);
                timeTrackerModel.TotalHours = differ.TotalHours;
                timeTrackerModel.TrackId = id;

                bool success = await _attendenceRepo.UpdateTrack(timeTrackerModel);
                if (success == true)
                {
                    return RedirectToAction(nameof(Portal));
                }
            }
            return View();
        }

        public async Task<IActionResult> BreakTime(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Portal), new { isFail = true });
            }
             else   
            {
                bool success = await _attendenceRepo.StartBreak(id);
                if (success == true)
                {
                    return RedirectToAction(nameof(Portal));
                }

            }
              
            return View();
        }

        public async Task<IActionResult> BreakEndTime(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Portal), new { isFail = true });
            }
            else
            {
                TimeTrackerModel timeTrackerModel = new TimeTrackerModel();
                var track = await _context.TimeTracker.FindAsync(id);
                TimeSpan differ = (TimeSpan)(DateTime.Now - track.BreakStart);
                timeTrackerModel.BreakingHours = differ.TotalHours;
                timeTrackerModel.TrackId = id;

                bool success = await _attendenceRepo.EndBreak(timeTrackerModel);
                if (success == true)
                {
                    return RedirectToAction(nameof(Portal));
                }
            }
            return View();
        }
    }
}
