using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.EmployeeServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages.EmployeeModule.List.child
{
    public class DisplayEmployeeBase : ComponentBase
    {

        [Parameter]
        public Employee Employee { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        [Parameter]
        public bool IsSeletced { get; set; } = true;

        [Parameter]
        public EventCallback<bool> EmployeeSelection { get; set; }

       
        [Inject]
        public IEmployeeService _EmployeeService { get; set; }

        [Parameter]
        public EventCallback<string> stringDelete { get; set; }

        protected PragimTech.Components.ConfirmBase DeleteConfirmation { get; set; }
        public void DeleteEmployee()
        {
            DeleteConfirmation.Show();

        }
        protected async Task ConfirmDelete_click(bool deleteConfirm)
        {
            if (deleteConfirm)
            {
                await _EmployeeService.DeleteEmployee(Employee.EmployeeId);
                await stringDelete.InvokeAsync(Employee.EmployeeId);
            }
        }
        //public async Task DeleteEmployee()
        //{
        //    await _EmployeeService.DeleteEmployee(Employee.EmployeeId);
        //    await stringDelete.InvokeAsync(Employee.EmployeeId);

        //}
        public async Task checkboxchange(ChangeEventArgs e)
        {
            IsSeletced =bool.Parse(e.Value.ToString());
           await EmployeeSelection.InvokeAsync(IsSeletced);
        }

    }
}
