using AppDev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using AppDev2.Repositories.Interface;

namespace AppDev2.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private ApplicationDbContext _context;

		public ProductRepository()
		{
			_context = new ApplicationDbContext();
		}

		public bool CheckExistProductName(string name)
		{
			return _context.Products.Any(p => p.Name.Contains(name));
		}

		public void CreateProduct(Product product)
		{
			var newProduct = new Product
			{
				Name = product.Name,
				CategoryId = product.CategoryId,
				Price = product.Price
			};

			_context.Products.Add(newProduct);
			_context.SaveChanges();

		}

		public bool DeleteProductById(int id)
		{
			var productInDb = GetProductById(id);

			if (productInDb == null) return false;

			_context.Products.Remove(productInDb);
			_context.SaveChanges();

			return true;
		}

		public bool EditProduct(Product product)
		{
			var productInDb = GetProductById(product.Id);

			if (productInDb == null) return false;

			productInDb.Name = product.Name;
			productInDb.Price = product.Price;
			productInDb.CategoryId = product.CategoryId;

			_context.SaveChanges();

			return true;
		}

		public bool EditProductById(int id, Product product)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetAllProducts()
		{
			return _context.Products
			.Include(p => p.Category);
		}

		public IEnumerable<Product> GetAllProductsWithSearchString(string searchString)
		{
			IEnumerable<Product> products = GetAllProducts();
			return products.Where(
					s => s.Name.Contains(searchString) ||
					s.Category.Name.Contains(searchString));
		}

		public Product GetProductById(int id)
		{
			return _context.Products.SingleOrDefault(p => p.Id == id);
		}
	}	
}