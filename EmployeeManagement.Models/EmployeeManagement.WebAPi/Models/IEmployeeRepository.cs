using Microsoft.AspNetCore.Mvc;
using Models.model;
using System.Collections.Generic;
namespace EmployeeManagement.WebAPi.Models
{
    public interface IEmployeeRepository
    {
		Task<IEnumerable<Employee>> GetEmployees();
		Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
		Task<Employee> DeleteEmployee(int id);
		Task<IEnumerable<Employee>> Search(string name, Gender? gender);
	}
}
