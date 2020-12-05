using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class UpcomingLeaveViewComponent : ViewComponent
    {
        private readonly LeaveRepository _leaveRepository;

        public UpcomingLeaveViewComponent(LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _leaveRepository.UpcomingLeaveAsync();
            return View(data);
        }
    }
}