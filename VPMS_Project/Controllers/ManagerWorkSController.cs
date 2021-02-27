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
    public class ManagerWorkSController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly TaskRepo _taskRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ManagerWorkSController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, TaskRepo taskRepo)
        {
            _empRepository = empRepository;
            _taskRepository = taskRepo;
            _webHostEnvironment = webHostEnvironment;

        }
        [HttpGet]
        public async Task<IActionResult> ViewWorkS(DateTime Date)
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
            if (Date == DateTime.MinValue)
            {
                ViewBag.Empty = true;
                return View();
            }
            else
            {
                ViewBag.Date = Date;
                var data = await _taskRepository.GetWorkSheet(Date);
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
