using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Entities;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKeyForCart = "Cart";


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = controllerContext.HttpContext.Session[sessionKeyForCart] as Cart;
            if(cart==null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKeyForCart] = cart;
            }
            return cart;
        }





    }
}