using EmployeeManagement.Web.Service;
using Microsoft.AspNetCore.Components;
using Models.model;

namespace EmployeeManagement.Web.Pages
{
	public class EditEmployeeBase:ComponentBase
	{
	
			public Employee Employee { get; set; } = new Employee();

			[Inject]
			public IEmployeeService EmployeeService { get; set; }

			[Parameter]
			public string Id { get; set; }

			protected async override Task OnInitializedAsync()
			{
				Employee = await EmployeeService.GetEmployee(int.Parse(Id));
			}
	}
}
