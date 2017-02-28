using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext  bindingContext )
        {
            //get the cart from the sessoin
            Cart cart = null;

            if (controllerContext.HttpContext.Session != null)
                cart = (Cart) controllerContext.HttpContext.Session[sessionKey];
            
            //create the cart if there wasn't on in the session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return cart;
        }
    }
}