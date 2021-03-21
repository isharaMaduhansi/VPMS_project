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
        private readonly TaskRepo _taskRepository = null;

        public AdminTimeSController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, TaskRepo taskRepository, JobRepository jobRepository)
        {
            _empRepository = empRepository;
            _jobRepository = jobRepository;
            _webHostEnvironment = webHostEnvironment;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ViewTimeSheet(DateTime Date, int Search)
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
            if (Date == DateTime.MinValue && Search == 0)
            {
                ViewBag.Empty = true;
                return View();
            }
            else
            {
                ViewBag.Date = Date;

                ViewBag.TotalHours = _taskRepository.GetTotalHours(Search, Date);
                var data = await _taskRepository.GetTimeSheet(Search, Date);
                if (data != null)
                {
                    ViewBag.Empty = false;
                    return View(data);
                }
                else
                {
                    ViewBag.Empty = true;
                    return View();
                }
            }
        }
    }
}
