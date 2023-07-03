using Models.model;

namespace EmployeeManagement.Web.Service
{
	public interface IEmployeeService
	{
		Task<IEnumerable<Employee>> GetEmployees();
		Task<Employee> GetEmployee(int id);

	}
}
