using ApplianceAndElectronicStore.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        Cart cart;

        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            string url = HttpContext.Request.GetEncodedUrl();

            ViewBag.ReturnUrl = url.Contains("/Cart") || url.Contains("/Orders/Checkout") ?
                    $"{HttpContext.Request.Query["returnUrl"]}" :
                    HttpContext.Request.GetEncodedPathAndQuery();

            return View(cart);
        }
    }
}
