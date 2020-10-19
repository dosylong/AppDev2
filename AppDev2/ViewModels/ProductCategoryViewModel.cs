using AppDev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDev2.ViewModels
{
	public class ProductCategoryViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}