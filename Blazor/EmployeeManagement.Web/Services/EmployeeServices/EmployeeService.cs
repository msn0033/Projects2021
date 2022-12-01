using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Employee> CreateEmplyee(Employee newemployee)
        {
            return await httpClient.PostJsonAsync<Employee>("api/employees", newemployee);
        }

        public async Task<Employee> GetEmployee(string Id)
        {
            return await httpClient.GetJsonAsync<Employee>($"api/employees/{Id}");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await httpClient.GetJsonAsync<Employee[]>("api/employees");
        }

        public async Task<Employee> UpdateEmployee(Employee UpdateEmployee)
        {
            return await httpClient.PutJsonAsync<Employee>("api/employees", UpdateEmployee);
        }

        public async Task DeleteEmployee(string employeeId)
        {
            await httpClient.DeleteAsync($"api/employees/{employeeId}");
        }

    }
}
