using Microsoft.AspNetCore.Http.HttpResults;
using Models.model;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPi.Models
{
	public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

		public async Task<IEnumerable<Employee>> GetEmployees()
		{
            dynamic employee = null;
			try
			{
				dynamic employees =  appDbContext.Employees.ToList();
				return employees;
			}
			catch (Exception ex)
			{

                return employee;
			}
		}


		public async Task<Employee> GetEmployee(int employeeId)
        {
            dynamic result = null;
            try
            {
				var results =appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

				Employee emp = new Employee();
				return results;
			}
            catch (Exception ex)
            {
                return result;
            }
         

        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = appDbContext.Employees.Add(employee);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result =  appDbContext.Employees
                .FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                  
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

		
		public async Task<Employee> DeleteEmployee(int employeeId)
		{
			var result = await appDbContext.Employees
				.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
			if (result != null)
			{
				appDbContext.Employees.Remove(result);
				await appDbContext.SaveChangesAsync();
				return result;
			}

			return null;
		}


		public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
		{
			IQueryable<Employee> query = appDbContext.Employees;

			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(e => e.FirstName.Contains(name)
							|| e.LastName.Contains(name));
			}

			if (gender != null)
			{
				query = query.Where(e => e.Gender == gender);
			}

			return await query.ToListAsync();
		}

	}
}
