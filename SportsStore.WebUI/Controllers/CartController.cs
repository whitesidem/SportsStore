using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportStore.Domain.Concrete;
using SportStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private IOrderProcessor _orderProcessor;

        /// <summary>
        /// Constructor - with dependency injection
        /// Both params come from Ninject binding (Infrastructure folder)
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="orderProcessor"></param>
        public CartController(IProductRepository repository, IOrderProcessor orderProcessor )
        {
            _repository = repository;
            _orderProcessor = orderProcessor;
        }


        /// <summary>
        /// Note: cart is not supplied - this is auto  instantiated using binding (ultimately getting it from session)
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel() {Cart = cart, ReturnUrl = returnUrl});

        }


        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }
        
        
        /// <summary>
        /// Note - do not pass cart parameter this uses model binding to auto instantiate
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID.Equals(productId));
            if(product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new {returnUrl});      //Return to Index method using previously stored url
        }

        /// <summary>
        /// Note - do not pass cart parameter this uses model binding to auto instantiate
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID.Equals(productId));
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });      //Return to Index method using previously stored url
        }


        /// <summary>
        /// Go to checkout view
        /// GET method.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }


        /// <summary>
        /// Post back for Checkout form
        /// </summary>
        /// <param name="cart">From Model Binding - see Global.asax</param>
        /// <param name="shippingDetails">From the posted form (this was the bound model type)</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.Lines.Count==0)
            {
                ModelState.AddModelError("","sorry, your cart is empty");
            }

            //Do not continue if model is in invalid state - i.e. must have no model errors - inc those added via attribute rules on the model
            if(ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart,shippingDetails);

                TempData["cartTotal"] = cart.ComputeTotalValue();
                
                cart.Clear();
                //Success - go to Completed View
                //Use Post/Redirect/Get patter to avoid problems with page refresh re-triggering post!!
                //Redirect loses info - so rebind what we need to the new request


                //Options are using session or passing simple types around as parameters - we use parameters here
                return RedirectToAction("ShowAsCompleted", new {shippedToName=shippingDetails.Name}); 
            }
            else
            {
                //Erros exist - so re-ender page with errors highlighted 
                return View(shippingDetails);
            }

        }

        /// <summary>
        /// Show thankyou message
        /// </summary>
        /// <returns></returns>
        public ViewResult ShowAsCompleted(string shippedToName)
        {
            ViewBag.ShippedToName = shippedToName;
            return View("Completed");
        }
    }
}
