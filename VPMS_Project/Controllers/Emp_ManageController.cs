using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class Emp_ManageController : Controller
    {
        private readonly EmpRepository _empRepository = null;
        private readonly JobRepository _jobRepository = null;

        public Emp_ManageController(EmpRepository empRepository, JobRepository jobRepository) 
        {
            _empRepository = empRepository;
            _jobRepository = jobRepository;
        }

        public async Task<IActionResult> ViewAllEmp()
        {
            var data= await _empRepository.GetAllEmps();
            return View(data);
        }

        [Route("Employee-Details/{id}",Name="empDetailsRoute")]
        public async Task<IActionResult> ViewEmpById(int id)
        {
           
           var data=await _empRepository.GetEmpById(id);
             return View(data);
        }

        public async Task<IActionResult> AddEmployee(bool isSucceess=false,int empId = 0)
        {
            var emp = new EmpModel();

            ViewBag.JobType = new SelectList(await _jobRepository.GetJobTypes(),"Id","name");
            ViewBag.IsSuccess = isSucceess;
            ViewBag.empId = empId;
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmpModel empModel)
        {
          
            if(ModelState.IsValid)
            {
                int id = await _empRepository.AddEmp(empModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddEmployee), new { isSucceess = true, empId = id });
                }
            }
            ViewBag.JobType = new SelectList(await _jobRepository.GetJobTypes(), "Id", "Name");
            return View();
        }
    }
}
