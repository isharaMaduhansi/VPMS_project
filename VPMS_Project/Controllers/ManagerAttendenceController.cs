using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class ManagerAttendenceController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly AttendenceRepo _attendenceRepo = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ManagerAttendenceController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, AttendenceRepo attendenceRepo)
        {
            _empRepository = empRepository;
            _attendenceRepo = attendenceRepo;
            _webHostEnvironment = webHostEnvironment;

        }
        [HttpGet]
        public async Task<IActionResult> Attendence(DateTime Date, int Search, int id = 0)
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
            ViewBag.Id = id;
            if (Date == DateTime.MinValue)
            {
                var data = await _attendenceRepo.SearchLeave1(Search);
                return View(data);
            }
            else if (Search == 0)
            {
                var data = await _attendenceRepo.SearchLeave3(Date);
                return View(data);

            }
            else
            {
                var data = await _attendenceRepo.SearchLeave2(Search, Date);
                return View(data);

            }

        }

        public async Task<IActionResult> NotUpdated()
        {
            var data = await _attendenceRepo.EmpNotUpdate();
            return View(data);
        }
    }
}
