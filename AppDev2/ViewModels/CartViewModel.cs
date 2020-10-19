using AppDev2.Models;
using System.Collections.Generic;

namespace AppDev2.ViewModels
{
	public class CartViewModel
	{
		public Product Product { get; set; }
		public Cart Cart { get; set; }
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Cart> Carts { get; set; }
	}
}