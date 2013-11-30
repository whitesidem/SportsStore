using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository _repository;

        public NavController(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This is a child controller and the category param is automatically passed on from parent Controller 
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public PartialViewResult Menu(string Category = null)
        {
            ViewBag.SelectedCategory = Category;

            IEnumerable<string> categories = _repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(cat => cat);
            return PartialView(categories);
        }

    }
}
