using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class Emp_ManageController : Controller
    {
        private readonly EmpRepository _empRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public Emp_ManageController(EmpRepository empRepository, IWebHostEnvironment webHostEnvironment) 
        {
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;


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

        public IActionResult AddEmployee(bool isSucceess=false,int empId = 0)
        {
            var emp = new EmpModel();
            ViewBag.IsSuccess = isSucceess;
            ViewBag.empId = empId;
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmpModel empModel)
        {
          
            if(ModelState.IsValid)
            {
                if (empModel.ProfilePhoto!=null) 
                {
                    String folder = "images/Employees/";
                    folder += Guid.NewGuid().ToString() + "_" + empModel.ProfilePhoto.FileName;
                    empModel.PhotoURL = "/"+folder;
                    String serverFolder = Path.Combine(_webHostEnvironment.WebRootPath,folder);

                    await empModel.ProfilePhoto.CopyToAsync(new FileStream(serverFolder,FileMode.Create));
                }
                int id = await _empRepository.AddEmp(empModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddEmployee), new { isSucceess = true, empId = id });
                }
            }
            return View();
        }
    }
}
