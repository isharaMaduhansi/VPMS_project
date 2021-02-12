using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class LeaveBalanceViewComponent : ViewComponent
    {
        private readonly LeaveRepository _leaveRepository;

        public LeaveBalanceViewComponent(LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _leaveRepository.LeaveBalanceGetAsync(id);
            return View(data);
        }
    }
}
