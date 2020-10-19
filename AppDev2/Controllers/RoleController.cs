using AppDev2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace AppDev2.Controllers
{
	public class RoleController : Controller
	{
		ApplicationDbContext context;

		public RoleController()
		{
			context = new ApplicationDbContext();
		}



		/// <summary>
		/// Get All Roles
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "admin")]
		public ActionResult Index()
		{
			var Roles = context.Roles.ToList();
			return View(Roles);
		}

		/// <summary>
		/// Create  a New role
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "admin")]
		public ActionResult Create()
		{
			var Role = new IdentityRole();
			return View(Role);
		}

		/// <summary>
		/// Create a New Role
		/// </summary>
		/// <param name="Role"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult Create(IdentityRole Role)
		{
			context.Roles.Add(Role);
			context.SaveChanges();
			return RedirectToAction("Index");
		}


	}
}