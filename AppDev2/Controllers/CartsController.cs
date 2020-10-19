using AppDev2.Models;
using AppDev2.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AppDev2.Controllers
{
	public class CartsController : Controller
	{
		private readonly ApplicationDbContext _context;
		public CartsController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Carts
		[Authorize(Roles = "admin")]
		public ActionResult Index()
		{
			var carts = _context.Carts
				.Include(s => s.Customer)
				.Include(s => s.Product)
				.ToList();
			return View(carts);
		}

		[Authorize(Roles = "guest")]
		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new CartViewModel
			{
				Products = _context.Products.ToList()
			};


			return View(viewModel);
		}

		[Authorize(Roles = "guest")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Product product)
		{
			Cart cart = new Cart
			{
				CustomerId = User.Identity.GetUserId(),
				ProductId = product.Id
			};

			_context.Carts.Add(cart);
			_context.SaveChanges();
			return RedirectToAction("Mine");
		}

		[HttpGet]
		[Authorize(Roles = "guest")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var carts = _context.Carts
				.Where(c => c.CustomerId == userId)
				.Include(c => c.Product)
				.ToList();

			return View(carts);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var cartInDb = _context.Carts.SingleOrDefault(p => p.Id == id);
			if (cartInDb == null)
			{
				return HttpNotFound();
			}
			_context.Carts.Remove(cartInDb);
			_context.SaveChanges();

			if (Request.IsAuthenticated && User.IsInRole("admin"))
			{
				return RedirectToAction("Index");
			}
			if (Request.IsAuthenticated && User.IsInRole("guest"))
			{
				return RedirectToAction("Mine");
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Authorize(Roles = "guest")]
		public ActionResult Edit(int id)
		{
			var cartInDb = _context.Carts.SingleOrDefault(p => p.Id == id);

			if (cartInDb == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CartViewModel
			{
				Carts = _context.Carts.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "guest")]
		public ActionResult Edit(Cart cart)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var cartInDb = _context.Carts.SingleOrDefault(p => p.Id == cart.Id);

			if (cartInDb == null)
			{
				return HttpNotFound();
			}

			cartInDb.Id = cart.Id;
			cartInDb.CustomerId = cart.CustomerId;
			cart.ProductId = cart.ProductId;

			_context.SaveChanges();

			return RedirectToAction("Mine");
		}
	}
}