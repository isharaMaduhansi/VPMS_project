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
    public class ManagerDashController : Controller
    {
        private readonly IEmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ManagerDashController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment)
        {
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.Emps = new SelectList(await _empRepository.GetEmps(), "EmpId", "EmpFullName");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmp(EmpModel empModel)
        {
            var data = await _empRepository.GetEmpById(empModel.EmpId);
            return View(data);
        }

       
    }
}
