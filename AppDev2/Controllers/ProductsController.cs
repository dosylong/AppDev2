using AppDev2.Models;
using AppDev2.Repositories.Interface;
using AppDev2.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AppDev2.Controllers
{
	public class ProductsController : Controller
	{
		private IProductRepository _productRepos;
		private ICategoryRepository _categoryRepos;

		public ProductsController(IProductRepository productRepos, ICategoryRepository categoryRepository)
		{
			_productRepos = productRepos;
			_categoryRepos = categoryRepository;
		}

		// GET: Products
		[HttpGet]
		public ActionResult Index(string searchString)
		{
			if (!String.IsNullOrEmpty(searchString))
			{
				return View(_productRepos.GetAllProductsWithSearchString(searchString).ToList());
			}

			return View(_productRepos.GetAllProducts().ToList());
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult Create()
		{
			var viewModel = new ProductCategoryViewModel
			{
				Categories = _categoryRepos.GetCategories()
			};
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (_productRepos.CheckExistProductName(product.Name))
			{
				ModelState.AddModelError("Name", "Product Name Already Exists.");
				return View();
			}

			_productRepos.CreateProduct(product);

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "admin")]

		public ActionResult Delete(int id)
		{
			if (!_productRepos.DeleteProductById(id))
			{
				return HttpNotFound();
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "admin")]
		public ActionResult Edit(int id)
		{
			var productInDb = _productRepos.GetProductById(id);

			if (productInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new ProductCategoryViewModel
			{
				Product = productInDb,
				Categories = _categoryRepos.GetCategories()
			};

			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public ActionResult Edit(Product product)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (!_productRepos.EditProduct(product))
			{
				return HttpNotFound();
			}

			return RedirectToAction("Index");
		}
	}
}