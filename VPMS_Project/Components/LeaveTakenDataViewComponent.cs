using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Components
{
    public class LeaveTakenDataViewComponent : ViewComponent
    {
        private readonly LeaveRepository _leaveRepository;

        public LeaveTakenDataViewComponent(LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var data = await _leaveRepository.GetLeaveTakenAsync(id);
            return View(data);
        }
    }
}
