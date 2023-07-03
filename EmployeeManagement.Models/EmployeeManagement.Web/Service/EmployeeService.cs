using Microsoft.AspNetCore.Mvc;
using Models.model;
using System.Net;

namespace EmployeeManagement.Web.Service
{
	public class EmployeeService:IEmployeeService
	{
		private readonly HttpClient httpClient;

		public EmployeeService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<IEnumerable<Employee>> GetEmployees()
		   {
			HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7032/api/Employee/GetEmployees");

			if (response.IsSuccessStatusCode)
			{
				IEnumerable<Employee> employees = await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
				return employees;
			}
			else
			{
				return Enumerable.Empty<Employee>();
			}
		}
		public async Task<Employee> GetEmployee(int id)
		{
			HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7032/api/Employee/GetEmployee/{id}");

			if (response.IsSuccessStatusCode)
			{
				Employee employee = await response.Content.ReadFromJsonAsync<Employee>();
				return employee;
			}
			else if (response.StatusCode == HttpStatusCode.NotFound)
			{
				// Handle the case when the employee is not found
				return null;
			}
			else
			{
				// Handle other error cases
				throw new Exception($"Error retrieving employee. Status code: {response.StatusCode}");
			}
		}

	}
}
