using AppDev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev2.Repositories.Interface
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> GetCategories();
	}
}
