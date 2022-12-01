using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> Search( string name,Gender? gender);
        Task<Employee> GetEmployee(string employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmplyee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(string employeeId);


    }
}
