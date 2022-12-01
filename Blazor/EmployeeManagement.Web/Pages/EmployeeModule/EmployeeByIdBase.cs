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
    public class EmployeeByIdBase : ComponentBase
    {

        [Inject] public IEmployeeService Em { get; set; }
        [Parameter] public string Id { get; set; }
        public Employee Employee { get; set; } = new Employee();
        protected override async Task OnInitializedAsync()
        {
            Employee = await Em.GetEmployee(Id);
        }

    }
}
