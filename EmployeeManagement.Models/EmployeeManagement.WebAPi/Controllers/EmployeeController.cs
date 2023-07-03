using EmployeeManagement.WebAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Models.model;

namespace EmployeeManagement.WebAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                dynamic data = await employeeRepository.GetEmployees();

				return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Employee>>CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                var createdEmployee = await employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }


		[HttpPut("{id:int}")]
		public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
		{
			try
			{
				if (id != employee.EmployeeId)
					return BadRequest("Employee ID mismatch");

				var employeeToUpdate = await employeeRepository.GetEmployee(id);

				if (employeeToUpdate == null)
					return NotFound($"Employee with Id = {id} not found");

				return await employeeRepository.UpdateEmployee(employee);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error updating data");
			}
		}


		[HttpDelete("{id:int}")]
		public async Task<ActionResult<Employee>> DeleteEmployee(int id)
		{
			try
			{
				var employeeToDelete = await employeeRepository.GetEmployee(id);

				if (employeeToDelete == null)
				{
					return NotFound($"Employee with Id = {id} not found");
				}

                return await employeeRepository.DeleteEmployee(id);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error deleting data");
			}
		}

		[HttpGet("{search}")]
		public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
		{
			try
			{
				var result = await employeeRepository.Search(name, gender);

				if (result.Any())
				{
					return Ok(result);
				}

				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
		}
	}

}
