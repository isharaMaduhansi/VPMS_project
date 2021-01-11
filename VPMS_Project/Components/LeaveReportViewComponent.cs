using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class LeaveReportViewComponent : ViewComponent
    {
        private readonly LeaveRepository _leaveRepository;

        public LeaveReportViewComponent(LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _leaveRepository.GetSearchLeaveAsync(id);
            return View(data);
        }
    }
}
