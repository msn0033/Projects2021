using EmployeeManagement.Api.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this._EmployeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                return Ok(await _EmployeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string Id)
        {
            try
            {
                var result = await _EmployeeRepository.GetEmployee(Id);
                if (result == null) return NotFound();//404
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost]//AddNew
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null) return BadRequest();
                var email = await _EmployeeRepository.GetEmployeeByEmail(employee.Email);
                if (email != null)
                {
                    ModelState.AddModelError("email", "Email already exists");
                    return BadRequest(ModelState);
                }


                var CreatedEmployee = await _EmployeeRepository.AddEmplyee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { Id = CreatedEmployee.EmployeeId }, CreatedEmployee);//201
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + "Error addnew employee");
            }
        }

        [HttpPut]//Update
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {

                {
                    var updateEmployee = await _EmployeeRepository.GetEmployee(employee.EmployeeId);
                    if (updateEmployee == null)
                        return NotFound($"Employee With Id={employee.EmployeeId} not found");

                    return await _EmployeeRepository.UpdateEmployee(employee);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erorr Updating data");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(string id)
        {
            try
            {
                var employeeDelete = await _EmployeeRepository.GetEmployee(id);
                if (employeeDelete == null)
                {
                    return NotFound($" Employee with  Id = {id} not found");
                }
                return await _EmployeeRepository.DeleteEmployee(id);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erorr deleting data");

            }


        }
        [HttpGet(nameof(Search))]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name , Gender? gender)
        {

            try
            {
                var result = (await _EmployeeRepository.Search(name, gender)).ToList();

                if (result.Any())
                    return result;
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erorr deleting data");
            }
            
        }
    }
}
