using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DotNetCore5_CRUD.Models
{
    
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name="Employee Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }
    }
}
