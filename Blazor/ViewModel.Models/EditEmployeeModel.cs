using EmployeeManagement.Models;
using EmployeeManagement.Models.CustomValidate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class EditEmployeeModel
    {
        public string EmployeeId { get; set; }
        [Required(ErrorMessage ="اسمك الاول")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "اسم الاب")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "البريد الالكتروني")]
        [EmailAddress(ErrorMessage ="البريد الالكتروني غير صحيح")]
        [EmailDomainValidate(AllowedDomain ="Gmail.com",ErrorMessage ="الدومين مختلف")]
        public string Email { get; set; }

        [CompareProperty(nameof(Email), ErrorMessage = "البريد الالكتروني غير مطابق")]
        public string ConfirmEmail { get; set; }

        public DateTime DateOfBrith { get; set; }

        public Gender Gender { get; set; }
        public string DepartmentId { get; set; }
        public string PhotoName { get; set; }
        [ValidateComplexType]
        public Department Department { get; set; }
    }
}
