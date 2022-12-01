using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using EmployeeManagement.Web.Services.EmployeeServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeList :ComponentBase
    {
        [Inject]
        public IEmployeeService _EmployeeService { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        public bool popup { get; set; }
        public bool ShowFooter { get; set; } = true;
        public bool Isselected { get; set; }
        public int Counter { get; set; } = 0;
        protected override async Task OnInitializedAsync()
        {
            Employees=(await _EmployeeService.GetEmployees()).ToList();
        }
        public async Task AfterDeleteGetList()
        {
            Employees = await _EmployeeService.GetEmployees();
        }
        public void counterpluse(bool te)
        {
            Isselected = te;
            if (Isselected)
            {
                Counter++;
            }
            else
                Counter--;

        }
  
    }
}
