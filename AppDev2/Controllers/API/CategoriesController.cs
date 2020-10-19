using AppDev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AppDev2.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext _context;
        public CategoriesController()
		{
            _context = new ApplicationDbContext();
		}

        [HttpGet]
        public IHttpActionResult GetAllCategories()
		{
            var categories = _context.Categories.ToList();

            return Ok(categories);
		}


    }
}
