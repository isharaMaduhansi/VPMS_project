using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class StaffLeaveController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LeaveRepository _leaveRepository = null;


        public StaffLeaveController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, LeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;

        }


        public async Task<IActionResult> Leavebalance()
        {
            int EmpId = 110;
            var data = await _leaveRepository.GetLeaveBalance(EmpId);
            return View(data);
        }


        public async Task<IActionResult> PendingLeaveHistory(bool isDelete = false)
        {
            int EmpId = 110;
            ViewBag.IsDelete = isDelete;
            var data = await _leaveRepository.GetAllPendingLeaveById(EmpId);
            return View(data);
        }
       
         public async Task<IActionResult> ApprovedHistory(bool isMore = false)
        {
            int EmpId = 110;
            ViewBag.IsMore = isMore;
            var data = await _leaveRepository.GetApprovedLeaveById(EmpId);
            return View(data);
        }
        
         public async Task<IActionResult> RejectedLeave()
        {
            int EmpId = 110;
            var data = await _leaveRepository.GetRejectedLeaveById(EmpId);
            return View(data);
        }
        public async Task<IActionResult> LeaveApply(bool isSucceess = false, bool isUpdate = false, int leaveId = 0, bool isExist=false)
        {
            int EmpId = 110;
            ViewBag.LeaveId = leaveId;
            ViewBag.IsSuccess = isSucceess;
            ViewBag.IsUpdate = isUpdate;
            ViewBag.IsExist = isExist;
            var data = await _leaveRepository.GetEmpLeaveById(EmpId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> LeaveApply(LeaveApplyModel leaveApplyModel)
        {
            leaveApplyModel.EmpId = 110;
            int Eid = leaveApplyModel.EmpId;
            DateTime date = leaveApplyModel.Startdate;
            bool existOne = _leaveRepository.CheckExist(Eid,date);
            if (existOne) 
            {
                return RedirectToAction(nameof(LeaveApply), new { isExist = true});

            }
            else {

                TimeSpan differ = (TimeSpan)(leaveApplyModel.EndDate - leaveApplyModel.Startdate);
                leaveApplyModel.NoOfDays = differ.Days;
                int id = await _leaveRepository.AddLeave(leaveApplyModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(LeaveApply), new { isSucceess = true, leaveId = id });
                }

            }
            return View();
        }

        public async Task<IActionResult> EditLeave(int id)
        {
            var data = await _leaveRepository.GetEmpLeaveJoinById(id);
            return View(data);
        }
        
        public async Task<IActionResult> ViewLeave(int id)
        {
            var data = await _leaveRepository.GetOneLeaveById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditLeave(LeaveApplyModel leaveApplyModel)
        {

            leaveApplyModel.EmpId = 110;
            TimeSpan differ = (TimeSpan)(leaveApplyModel.EndDate - leaveApplyModel.Startdate);
            leaveApplyModel.NoOfDays = differ.Days;
            bool success = await _leaveRepository.UpdateLeave(leaveApplyModel);

                if (success == true)
                {
                    return RedirectToAction(nameof(LeaveApply), new { isUpdate = true, leaveId = leaveApplyModel.LeaveApplyId });
                }

            return View();
        }

            public async Task<IActionResult> DeleteLeave(int id)
        {

            bool success = await _leaveRepository.DeleteLeave(id);
            if (success == true)
            {
                return RedirectToAction(nameof(PendingLeaveHistory), new { isDelete = true });

            }
            return View();
        }

      
        public async Task<IActionResult> ClearApprovedLeave(int id)
        {

            bool success = await _leaveRepository.ClearLeave(id);
            if (success == true)
            {
                return RedirectToAction(nameof(ApprovedHistory));

            }
            return View();
        }

        public async Task<IActionResult> ClearRejectLeave(int id)
        {

            bool success = await _leaveRepository.ClearLeave(id);
            if (success == true)
            {
                return RedirectToAction(nameof(RejectedLeave));

            }
            return View();
        }
    }
}
