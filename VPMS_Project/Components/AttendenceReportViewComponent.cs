using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class AttendenceReportViewComponent : ViewComponent
    {
        private readonly AttendenceRepo _attendenceRepository = null;

        public AttendenceReportViewComponent(AttendenceRepo attendenceRepo)
        {
            _attendenceRepository = attendenceRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _attendenceRepository.GetSearchAttendenceAsync(id);
            return View(data);
        }
    }
}