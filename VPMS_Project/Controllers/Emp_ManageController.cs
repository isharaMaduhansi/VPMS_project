using System;
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

        public Emp_ManageController() 
        {
            _empRepository = new EmpRepository();
        }

        public IActionResult ViewAllEmp()
        {
            //return _empRepository.GetAllEmps();
            return View();
        }

        public IActionResult ViewEmpById(int id)
        {
           // return _empRepository.GetEmpById(id);
            return View();
        }
    }
}
