using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VPMS_Project.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VPMS_Project.Models
{
    public class EmpModel
    {
        public int EmpId { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "First name field is required")]
        public String EmpFName { get; set; }



        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Last name field is required")]
        public String EmpLName { get; set; }



        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public String Email { get; set; }


        [Required(ErrorMessage = "Mobile number field is required")]
        public int? Mobile { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of birth field is required")]
        public DateTime Dob { get; set; }

        [DataType(DataType.Date)]
        [System.ComponentModel.DisplayName("dd/MM/yyyy")]
        [Required(ErrorMessage = "Join date field is required")]
        public DateTime WorkSince { get; set; }

        public String Address { get; set; }

        [Required(ErrorMessage = "Job title field is required")]
        public int JobTitleId { get; set; }

        public String JobType { get; set; }

        public IFormFile ProfilePhoto { get; set; }

        public String PhotoURL { get; set; }

        [Required(ErrorMessage = "Please select one")]
        public String Gender { get; set; }

        [Required(ErrorMessage = "Please select one")]
        public String Status { get; set; }

        public DateTime LastDayWorked { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime Todate { get; set; }
        
        public int MedicalAllocated { get; set; }
        public int CasualAllocated { get; set; }
        public int AnnualAllocated { get; set; }
        public int ShortLeaveAllocated { get; set; }
        public int HalfLeaveAllocated { get; set; }



    }
}
