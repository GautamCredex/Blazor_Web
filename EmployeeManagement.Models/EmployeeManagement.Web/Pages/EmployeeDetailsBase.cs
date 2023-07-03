using EmployeeManagement.Web.Service;

using Microsoft.AspNetCore.Components;
using Models.model;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
	public class EmployeeDetailsBase : ComponentBase
	{
		public Employee Employee { get; set; } = new Employee();

		[Inject]
		public IEmployeeService EmployeeService { get; set; }

		[Parameter]
		public string Id { get; set; }
		protected string Coordinates { get; set; }
		protected string ButtonText { get; set; } = "Hide Footer";
		protected string CssClass { get; set; } = null;
		protected override async Task OnInitializedAsync()
		{
			// If Id value is not supplied in the URL, use the value 1
			Id = Id ?? "1";
			Employee = await EmployeeService.GetEmployee(int.Parse(Id));
		}
		//protected void Mouse_Move(MouseEventArgs e)
		//{
		//	Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
		//}

		protected void Button_Click()
		{
			if (ButtonText == "Hide Footer")
			{
				ButtonText = "Show Footer";
				CssClass = "HideFooter";
			}
			else
			{
				CssClass = null;
				ButtonText = "Hide Footer";
			}
		}
	}
}