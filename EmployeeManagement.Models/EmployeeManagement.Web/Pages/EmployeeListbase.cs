using EmployeeManagement.Web.Service;
using Microsoft.AspNetCore.Components;
using Models.model;
using System.Reflection;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListbase : ComponentBase
    {
		[Inject]
		public IEmployeeService EmployeeService { get; set; }

		public IEnumerable<Employee> Employees { get; set; }

		public bool ShowFooter { get; set; } = true;

		protected int SelectedEmployeesCount { get; set; } = 0;

		protected void EmployeeSelectionChanged(bool isSelected)
		{
			if (isSelected)
			{
				SelectedEmployeesCount++;
			}
			else
			{
				SelectedEmployeesCount--;
			}
		}
		protected override async Task OnInitializedAsync()
		{
			Employees = (await EmployeeService.GetEmployees()).ToList();
		}

	}      


}
