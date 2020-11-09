using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VPMS_Project.Repository;

namespace VPMS_Project.Component
{
    public class EmpListViewComponent : ViewComponent
    {

        private readonly IEmpRepository _empRepository;

        public EmpListViewComponent(IEmpRepository empRepository)
        {
            _empRepository = empRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync(string name,string job,int id)
        {
            var emps = await _empRepository.GetEmpListAsync(name,job,id);
            return View(emps);
        }


    }
}
