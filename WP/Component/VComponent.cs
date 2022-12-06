using System;
using Microsoft.AspNetCore.Mvc;

namespace WP.Component
{
	public class VComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}

