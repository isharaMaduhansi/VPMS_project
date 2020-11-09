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
        private readonly IEmpRepository _empRepository = null;
        private readonly JobRepository _jobRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public Emp_ManageController(IEmpRepository empRepository, IWebHostEnvironment webHostEnvironment, JobRepository jobRepository) 
        {
            _empRepository = empRepository;
            _jobRepository = jobRepository;
            _webHostEnvironment = webHostEnvironment;


        }

        public async Task<IActionResult> ViewAllEmp()
        {
            var data= await _empRepository.GetActiveEmps();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> SearchEmp(string Search, bool isSucceess = false)
        {
            var data = await _empRepository.GetSearchEmps(Search);
            ViewBag.IsSuccess = isSucceess;
            if (data == null) 
            {
                return RedirectToAction(nameof(SearchEmp), new { isSucceess = true });
            }
            return View(data);
        }

        public async Task<IActionResult> SeeRemoveEmp()
        {
            var data = await _empRepository.GetDeletedEmps();
            return View(data);
        }

        public async Task<IActionResult> DeletedEmpById(int id)
        {
            var data = await _empRepository.GetEmpById(id);
            return View(data);
        }



        public async Task<IActionResult> EditEmp(bool isSucceess = false, int empId = 0)
        {
            ViewBag.IsSuccess = isSucceess;
            ViewBag.empId = empId;
            var data = await _empRepository.GetActiveEmps();
            return View(data);
        }

        public async Task<IActionResult> RemoveEmp(bool isSucceess = false)
        {
            ViewBag.IsSuccess = isSucceess;
            var data = await _empRepository.GetActiveEmps();
            return View(data);
        }

        public async Task<IActionResult> DeleteEmp(int id)
        {

            var data = await _empRepository.GetEmpById(id);
            return View(data);
        }

     
        public async Task<IActionResult> DeleteEmpPost(int id)
        {

            bool success = await _empRepository.DeleteEmp(id);
            if (success == true)
            {
                return RedirectToAction(nameof(RemoveEmp), new { isSucceess = true });

            }
            return View();
        }




        [Route("~/Employee-Details/{id}",Name="empDetailsRoute")]
        public async Task<IActionResult> ViewEmpById(int id)
        {
           
           var data=await _empRepository.GetEmpById(id);
             return View(data);
        }

        public async Task<IActionResult> AddEmployee(bool isSucceess=false,int empId = 0)
        {
            var emp = new EmpModel();
            ViewBag.Jobs =new SelectList( await _jobRepository.GetJobs(),"JobId","JobName");
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
            ViewBag.Jobs = new SelectList(await _jobRepository.GetJobs(), "JobId", "JobName");
            return View();
        }
        


        public async Task<IActionResult> EditEmpById(int id)
        {
            ViewBag.empId = id;
            ViewBag.Jobs = new SelectList(await _jobRepository.GetJobs(), "JobId", "JobName");
            var data = await _empRepository.GetEmpById(id);
            return View(data);
        }


       
        [HttpPost]
        public async Task<IActionResult> EditEmpById(EmpModel empModel)
        {

            if (ModelState.IsValid)
            {
                if (empModel.ProfilePhoto != null)
                {
                    String folder = "images/Employees/";
                    folder += Guid.NewGuid().ToString() + "_" + empModel.ProfilePhoto.FileName;
                    empModel.PhotoURL = "/" + folder;
                    String serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await empModel.ProfilePhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                bool success = await _empRepository.UpdateEmp(empModel);

                if (success == true)
                {
                    return RedirectToAction(nameof(EditEmp), new { isSucceess = true, empId = empModel.EmpId});
                }
            }
            ViewBag.Jobs = new SelectList(await _jobRepository.GetJobs(), "JobId", "JobName");
            return View();
        }

       
        public async Task<IActionResult> ModifyList(bool isSucceess = false,bool isdelete =false)
        {
            ViewBag.IsSuccess = isSucceess;
            ViewBag.IsDelete = isdelete;
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
                return RedirectToAction(nameof(ModifyList), new { isSucceess = true });
            }

            return View();
        }
        
             public async Task<IActionResult> DeleteJob(int id)
        {

            bool success = await _jobRepository.DeleteJob(id);
            if (success == true)
            {
                return RedirectToAction(nameof(ModifyList), new { isdelete = true });

            }
            return View();
        }


    }
}
