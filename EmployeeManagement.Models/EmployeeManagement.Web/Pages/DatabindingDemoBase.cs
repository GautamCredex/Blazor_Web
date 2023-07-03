﻿using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
	public class DatabindingDemoBase:ComponentBase
	{
		protected string Name { get; set; } = "Tom";
		protected string Gender { get; set; } = "Male";

		protected string Description { get; set; } = string.Empty;
	}
}
