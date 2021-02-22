using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Models;
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

        public async Task<IActionResult> TimeSheet(int id, DateTime Start,DateTime End ,String Complete,int ID = 0)
        {
            ViewBag.EmpId = 2;
            ViewBag.TaskId = ID;
         
            if (ID!=0) 
            {
                await _taskRepository.AddTaskFromList(ID);
            }
            if (Start != DateTime.MinValue && End != DateTime.MinValue)
            {
                TimeSpan differ = (TimeSpan)(End - Start);
                Double TotalHours = differ.TotalHours;
                await _taskRepository.TImeSheetTaskInsert(id, Start, End, TotalHours);
            }

            if (Complete== "Complete")
            {
                await _taskRepository.CompleteTask(id);
            }
            if (Complete == "NotComplete")
            {
                await _taskRepository.NotCompleteTask(id);
            }
            var data = await _taskRepository.AddTaskList(ViewBag.EmpId);
            return View(data);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            bool success = await _taskRepository.CancelAddingTask(id);

            if (success)
            {
                return RedirectToAction(nameof(TimeSheet));
            }
            return RedirectToAction(nameof(TimeSheet));
        }


    }
}
