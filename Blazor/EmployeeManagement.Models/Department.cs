using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
   public class Department
    {
       
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "اسم القسم")]
        public string DepartmentName { get; set; }
    }
}
