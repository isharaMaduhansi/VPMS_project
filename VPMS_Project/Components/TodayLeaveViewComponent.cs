using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class TodayLeaveViewComponent : ViewComponent
    {
        private readonly LeaveRepository _leaveRepository;

        public TodayLeaveViewComponent(LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _leaveRepository.TodayLeaveAsync();
            return View(data);
        }
    }
}
