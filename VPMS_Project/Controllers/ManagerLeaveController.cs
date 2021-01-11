using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class ManagerLeaveController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly LeaveRepository _leaveRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ManagerLeaveController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, LeaveRepository leaveRepository)
        {
            _empRepository = empRepository;
            _leaveRepository = leaveRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        [HttpGet]
        public async Task<IActionResult> Leave(DateTime Date, int Search,int id=0)
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
             ViewBag.Id = id;
            if (Date == DateTime.MinValue)
            {
                var data = await _leaveRepository.SearchLeave1(Search);
                return View(data);
            }
            else if (Search == 0)
            {
                var data = await _leaveRepository.SearchLeave3(Date);
                return View(data);

            }
            else
            {
                var data = await _leaveRepository.SearchLeave2(Search, Date);
                return View(data);

            }
           
        }
    }
}
