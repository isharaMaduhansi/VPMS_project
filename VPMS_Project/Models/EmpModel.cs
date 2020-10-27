using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS_Project.Models
{
    public class EmpModel
    {
        public int EmpId { get; set; }

        [StringLength(20,MinimumLength =3)]
        [Required(ErrorMessage ="Please Enter first name")]
        public String EmpFName { get; set; }
        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Please Enter last name")]
        public String EmpLName { get; set; }
        [Required(ErrorMessage = "Please Enter job title")]
        public String Position { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter email")]
        [EmailAddress]
        public String Email { get; set; }
        [Required(ErrorMessage = "Please Enter mobile number")]
        public int? Mobile { get; set; }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

       
        public DateTime? WorkSince { get; set; }
        public String Address { get; set; }


    }
}
