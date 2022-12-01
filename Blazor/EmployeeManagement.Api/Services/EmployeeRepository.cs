using EmployeeManagement.Api.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _AppDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this._AppDbContext = appDbContext;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _AppDbContext.Employees.ToListAsync();
        }
        public async Task<Employee> GetEmployee(string employeeId)
        {
            return await _AppDbContext.Employees.Include(x=>x.Department).FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }
        public async Task<Employee> AddEmplyee(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            var result = await _AppDbContext.Employees.AddAsync(employee);
            await _AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _AppDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);
            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.Gender = employee.Gender;
                result.DateOfBrith = employee.DateOfBrith;
                result.PhotoName = employee.PhotoName;
                await _AppDbContext.SaveChangesAsync();
            }

            return result;
        }
        public async Task<Employee> DeleteEmployee(string employeeId)
        {
            var result = await _AppDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (result != null)
            {
                _AppDbContext.Employees.Remove(result);
                await _AppDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var result = await _AppDbContext.Employees.FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> qurey = _AppDbContext.Employees;
            if(!string.IsNullOrEmpty(name))
            qurey = qurey.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            if (gender != null)
                qurey = qurey.Where(e => e.Gender == gender);
            return await qurey.ToListAsync();
        }
    }
}
