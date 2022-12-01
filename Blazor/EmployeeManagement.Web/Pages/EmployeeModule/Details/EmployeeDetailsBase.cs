using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using EmployeeManagement.Web.Services.EmployeeServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages.EmployeeModule.Details
{
    public class EmployeeDetailsBase :ComponentBase
    {
        
        public Employee Employee { get; set; } = new Employee();
        [Inject] public  IEmployeeService EmployeeService { set; get; }
        [Parameter]public string Id { get; set; }
        public string coordinate { get; set; }

        public string ButtonText { get; set; } = "Hide Footer";
        public string CssClass { get; set; }
        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                ButtonText = "Hide Footer";
                CssClass = null;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployee(Id);
        }

    }
}
