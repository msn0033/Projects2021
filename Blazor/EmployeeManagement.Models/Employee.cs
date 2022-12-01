using EmployeeManagement.Models.CustomValidate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
       
        public string EmployeeId { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

       
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }

        public Gender Gender { get; set; }
        public string DepartmentId { get; set; }
        public string PhotoName { get; set; }
        
        public Department Department { get; set; }
    }
}