using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.DepartmentServices;
using EmployeeManagement.Web.Services.EmployeeServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Models;

namespace EmployeeManagement.Web.Pages.EmployeeModule.Edit
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject] public IMapper _Mapper { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public IEmployeeService _EmployeeService { get; set; }
        [Inject]public IDepartmentService _Department { get; set; }
        
        [Parameter] public string Id { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        public List<Department> Department { get; set; } = new List<Department>();

        public string Tital { get; set; }
        protected async override Task OnInitializedAsync()
        {

            if(Id !=null)
            {
                Employee= await _EmployeeService.GetEmployee(Id);
                Tital = "تعديل مستخدم";
            }
            else
            {
                Employee = new Employee()
                {
                    DateOfBrith = DateTime.Now,
                    Gender=Gender.Male,
                    DepartmentId="1",
                    Email="@gmail.com",
                    PhotoName= "do-not-reply.png"

                };
                Tital = "أضافة مستخدم تجديد";
            }
            Department = (await _Department.GetDepartments()).ToList();
            _Mapper.Map(Employee, EditEmployeeModel);
        }
        protected async Task HandlelValidsubmit()
        {
            _Mapper.Map(EditEmployeeModel, Employee);
            Employee result = null;
            if(Id != null)
            {
                result= await _EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await _EmployeeService.CreateEmplyee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
         
        }

        protected async Task Delete_Employee()
        {
           await _EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");

        }
    }
}
