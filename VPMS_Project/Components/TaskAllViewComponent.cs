using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class TaskAllViewComponent : ViewComponent
    {
        private readonly TaskRepo _taskRepo;

        public TaskAllViewComponent(TaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _taskRepo.GetAllTaskListAsync(id);
            return View(data);
        }
    }
}