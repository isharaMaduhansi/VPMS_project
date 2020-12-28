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
        private readonly TimeTrackRepo _timeTrackRepo = null;
        private readonly EmpStoreContext _context = null;
        private readonly AttendenceRepo _attendenceRepo = null;
        


        public EmployeeHomeController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, TimeTrackRepo timeTrackRepo, EmpStoreContext context, AttendenceRepo attendenceRepo)
        {
            _timeTrackRepo = timeTrackRepo;
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _attendenceRepo = attendenceRepo;

        }

        public IActionResult EmpIndex()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Portal(bool isExist = false, bool isOutExist = false,bool isFail = false, bool isEnd=false)
        {
            int EmpId = 110;
            ViewBag.EmpId = EmpId;
            bool result = _timeTrackRepo.CheckExist(EmpId);

            if (result == true) 
            {
                ViewBag.EndBreak = isEnd;
                var data = await _timeTrackRepo.GetTime(EmpId);
                ViewBag.Status = data.Status;
                bool check = _timeTrackRepo.CheckOut(EmpId);
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
                  

                    if (data.Status=="Break")
                        ViewBag.StartBreak = "Break";
                    else
                        ViewBag.StartBreak = "Work";

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
                ViewBag.Status = "Not mark In-time";
                return View();
            }
            
        }

        public async Task<IActionResult> InTime(TimeTrackerModel timeTrackerModel)
        {
            timeTrackerModel.EmpId = 110;
            bool existOne = _timeTrackRepo.CheckIn(timeTrackerModel.EmpId);
            if (existOne)
            {
                return RedirectToAction(nameof(Portal), new { isExist = true });
            }
            else
            {
                int id = await _timeTrackRepo.InTimeMark(timeTrackerModel);
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
                if(track.Status!="Break")
                {
                    TimeSpan differ = (TimeSpan)(DateTime.Now - track.InTime);
                    timeTrackerModel.TotalHours = differ.TotalHours;
                    timeTrackerModel.TrackId = id;
                    timeTrackerModel.WorkingHours = differ.TotalHours-track.BreakingHours;

                    bool success = await _timeTrackRepo.UpdateTrack(timeTrackerModel);
                    if (success == true)
                    {
                        return RedirectToAction(nameof(Portal));
                    }
                }
                else
                    return RedirectToAction(nameof(Portal), new { isEnd = true });
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
                bool check = await _timeTrackRepo.CheckOut1(id);
                if (check == true)
                {
                    bool success = await _timeTrackRepo.StartBreak(id);
                    if (success == true)
                    {
                        return RedirectToAction(nameof(Portal));
                    }
              
                }
                else
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
                bool check =await _timeTrackRepo.CheckOut2(id);
                if (check == true)
                {

                    TimeTrackerModel timeTrackerModel = new TimeTrackerModel();
                    var track = await _context.TimeTracker.FindAsync(id);
                    TimeSpan differ = (TimeSpan)(DateTime.Now - track.BreakStart);
                    timeTrackerModel.BreakingHours = differ.TotalHours;
                    timeTrackerModel.TrackId = id;

                    bool success = await _timeTrackRepo.EndBreak(timeTrackerModel);
                    if (success == true)
                    {
                        return RedirectToAction(nameof(Portal));
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Portal));
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TimeInfo(int id)
        {
                var data = await _timeTrackRepo.TrackInfoById(id);
                return View(data);
    
        }
    }
}
