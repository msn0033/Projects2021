using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string Id);
        
        Task<Employee> UpdateEmployee(Employee UpdateEmployee);
        Task<Employee> CreateEmplyee(Employee newemployee);

        Task DeleteEmployee(string employeeId);

    }

}
