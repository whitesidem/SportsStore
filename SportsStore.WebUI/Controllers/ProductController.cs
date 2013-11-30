using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 2;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List(string category, int page=1)
        {
            ProductListViewModel viewModel = new ProductListViewModel();
            viewModel.Products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID).Skip((page - 1)*PageSize)
                .Take(PageSize);
            viewModel.PagingInfo = new PagingInfo()
                                       {
                                           CurrentPage = page,
                                           ItemsPerPage = PageSize,
                                           TotalItems = category == null ?  _repository.Products.Count() : _repository.Products
                                                                                                           .Where(p => p.Category == category).Count()
                                       };
            viewModel.CurrentCategory = category;

             return View(viewModel);
        }




    }
}
