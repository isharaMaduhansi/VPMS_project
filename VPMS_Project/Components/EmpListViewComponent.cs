﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Component
{
    public class EmpListViewComponent : ViewComponent
    {

        private readonly EmpRepository _empRepository;

        public EmpListViewComponent(EmpRepository empRepository)
        {
            _empRepository = empRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync(string name,string job)
        {
            var emps = await _empRepository.GetEmpListAsync(name,job);
            return View(emps);
        }
    }
}
