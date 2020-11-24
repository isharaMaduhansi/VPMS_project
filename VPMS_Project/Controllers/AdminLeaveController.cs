using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class AdminLeaveController : Controller

    {
        private readonly IEmpRepository _empRepository = null;
        private readonly JobRepository _jobRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LeaveRepository _leaveRepository = null;

        public AdminLeaveController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, LeaveRepository leaveRepository, JobRepository jobRepository)
        {
            _empRepository = empRepository;
            _jobRepository = jobRepository;
            _webHostEnvironment = webHostEnvironment;
            _leaveRepository = leaveRepository;
        }

        [HttpGet]
        public async Task<IActionResult> LeaveAllocation(string Search = null)
        {
            var data = await _empRepository.GetSearchEmps(Search);
          
            if (data == null )
            {
                return RedirectToAction(nameof(LeaveAllocation));
            }

            return View(data);
        }

        
            public async Task<IActionResult> HRLeaveTableSee(bool isSucceess = false, bool isUpdate = false, bool isDelete = false)
        {
            ViewBag.IsSuccess = isSucceess;
            ViewBag.IsUpdate = isUpdate;
            ViewBag.IsDelete = isDelete;
            var data = await _jobRepository.GetJobs();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddJob(JobModel jobModel)
        {
            int id = await _jobRepository.AddJob(jobModel);

            if (id > 0)
            {
                return RedirectToAction(nameof(HRLeaveTableSee), new { isSucceess = true });
            }

            return View();
        }

        public async Task<IActionResult> Recomend()
        {
            var data = await _leaveRepository.GetLeaveRecommend();
            return View(data);
        }

        public async Task<IActionResult> EditJob(int id)
        {
            var data = await _jobRepository.GetJobById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(JobModel jobModel)
        {

            if (ModelState.IsValid)
            {
               
                bool success = await _jobRepository.Updatejob(jobModel);

                if (success == true)
                {
                    return RedirectToAction(nameof(HRLeaveTableSee), new { isUpdate = true,});
                }
            }
            
            return View();
        }

        public async Task<IActionResult> DeleteJob(int id)
        {

            bool success = await _jobRepository.DeleteJob(id);
            if (success == true)
            {
                return RedirectToAction(nameof(HRLeaveTableSee), new { isDelete = true });

            }
            return View();
        }

        
            public async Task<IActionResult> SeeLeaveAllocation(int id, bool isUpdate = false)
        {
            ViewBag.IsUpdate = isUpdate;
            var data = await _empRepository.GetEmpById(id);
            return View(data);
        }

        public async Task<IActionResult> EditLeaveAllocation(int id)
        {
            var data = await _empRepository.GetEmpById(id);
            return View(data);
        }



        [HttpPost]
        public async Task<IActionResult> EditLeaveAllocation(EmpModel empModel)
        {

          
            
                bool success = await _empRepository.UpdateEmpLeave(empModel);

                if (success == true)
                {
                    return RedirectToAction(nameof(SeeLeaveAllocation),new {id= empModel.EmpId, isUpdate = true });
                }
            
            return View();
        }



    }
}
