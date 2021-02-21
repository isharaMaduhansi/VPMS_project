using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class StaffTimeSheetController :  Controller
        {
        private readonly IEmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TaskRepo _taskRepository = null;



        public StaffTimeSheetController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, TaskRepo taskRepository)
        {
            _taskRepository = taskRepository;
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;

        }


        public async Task<IActionResult> TimeSheet()
        {
            ViewBag.EmpId = 2;
            var data = await _taskRepository.AddTaskList(ViewBag.EmpId);
            return View(data);
        }

        public async Task<IActionResult> TaskComplete(int id)
        {

            bool success = await _taskRepository.CompleteTask(id);
            if (success == true)
            {
                return RedirectToAction(nameof(TimeSheet));

            }
            return View();
        }

        public async Task<IActionResult> TaskNotComplete(int id)
        {

            bool success = await _taskRepository.NotCompleteTask(id);
            if (success == true)
            {
                return RedirectToAction(nameof(TimeSheet));

            }
            return View();
        }
    }
}
