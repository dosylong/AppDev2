using AppDev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev2.Repositories.Interface
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetAllProducts();
		void CreateProduct(Product product);

		bool DeleteProductById(int id);

		IEnumerable<Product> GetAllProductsWithSearchString(string searchString);
		Product GetProductById(int id);
		bool EditProduct(Product product);
		bool CheckExistProductName(string name);
	}
}
