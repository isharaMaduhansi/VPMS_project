﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VPMS_Project.Models;
using VPMS_Project.Repository;

namespace VPMS_Project.Controllers
{
    public class Emp_ManageController : Controller
    {
        private readonly EmpRepository _empRepository = null;

        public Emp_ManageController(EmpRepository empRepository) 
        {
            _empRepository = empRepository;
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
            ViewBag.IsSuccess = isSucceess;
            ViewBag.bookId = empId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmpModel empModel)
        {
           int id= await _empRepository.AddEmp(empModel);
            if (id > 0)
            {
                return RedirectToAction(nameof(AddEmployee),new { isSucceess =true, empId =id });
            }
            return View();
        }
    }
}
