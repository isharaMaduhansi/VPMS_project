using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class ManagerTimeSController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly TaskRepo _taskRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ManagerTimeSController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, TaskRepo taskRepo)
        {
            _empRepository = empRepository;
            _taskRepository = taskRepo;
            _webHostEnvironment = webHostEnvironment;

        }
        [HttpGet]
        public async Task<IActionResult> ViewTimeS(DateTime Date, int Search)
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
            if (Date == DateTime.MinValue && Search==0)
            {
                ViewBag.Empty = true;
                return View();
            }
            else
            {
                ViewBag.Date = Date;
               
                ViewBag.TotalHours= _taskRepository.GetTotalHours(Search, Date);
                var data = await _taskRepository.GetTimeSheet(Search, Date);
                if (data != null)
                {
                    ViewBag.Empty = false;
                    return View(data);
                }
                else {
                    ViewBag.Empty = true;
                    return View();
                }


           

            }
           

        }
    }
}
