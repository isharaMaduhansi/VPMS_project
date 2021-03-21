using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VPMS_Project.Enums
{
    public enum EmployeeEnum
    {
        [Display(Name = "CEO")]
        CEO,

        [Display(Name = "Project Manager")]
        Prpject_Manager,
        Head_Of_IT,
        Project_Account_Manager,
        System_Architect,
        Team_Leader,
        Software_Engineer,
        Senior_Software_Engineer,
        BQA,
        QA,
        BA

    }
}