using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VPMS_Project.Enums;
using Microsoft.AspNetCore.Http;

namespace VPMS_Project.Models
{
    public class EmpModel
    {
        public int EmpId { get; set; }

        [StringLength(20,MinimumLength =3)]
        [Required(ErrorMessage = "Please Enter first name")]
        public String EmpFName { get; set; }



        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Please Enter last name")]
        public String EmpLName { get; set; }



        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter email")]
        [EmailAddress]
        public String Email { get; set; }


        [Required(ErrorMessage = "Please Enter mobile number")]
        public int? Mobile { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        public DateTime WorkSince { get; set; }

        public String Address { get; set; }

        [Required(ErrorMessage = "Please choose Job Title")]
        public String JobTitle { get; set; }

        public IFormFile ProfilePhoto { get; set; }

        public String PhotoURL { get; set; }

        [Required(ErrorMessage = "Please select one")]
        public String Gender { get; set; }



    }
}
