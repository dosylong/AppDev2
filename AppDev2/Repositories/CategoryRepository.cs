using AppDev2.Models;
using AppDev2.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDev2.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepository()
		{
			_context = new ApplicationDbContext();
		}
		public IEnumerable<Category> GetCategories()
		{
			return _context.Categories.ToList();
		}
	}
}