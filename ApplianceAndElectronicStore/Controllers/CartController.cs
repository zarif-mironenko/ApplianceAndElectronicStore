using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplianceAndElectronicStore.Data;
using ApplianceAndElectronicStore.Models;
using ApplianceAndElectronicStore.Models.Entities;
using ApplianceAndElectronicStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplianceAndElectronicStore.Controllers
{
    public class CartController : Controller
    {
        readonly Cart cart;
        readonly ApplicationDbContext context;

        public CartController(Cart cartService, ApplicationDbContext contextService)
        {
            cart = cartService;
            context = contextService;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public IActionResult AddToCart(int productId, int quantity, string returnUrl)
        {
            Product product = context.Products
                .Include(p => p.Manufacturer)
                .FirstOrDefault(p => p.Id == productId);

            if (product != null) {
                CartItem cartItem = cart.Items.FirstOrDefault(ci => ci.Product.Id == product.Id);

                if ((cartItem == null && quantity > product.Quantity) ||
                    (cartItem?.ProductQuantity + quantity) > product.Quantity) {
                    TempData["message"] = $"Максимально можно заказать <b>{product.Quantity} шт.</b>";
                    TempData["alertContextClass"] = "alert-danger";
                } else {
                    cart.AddItem(product, quantity);

                    TempData["message"] = new StringBuilder()
                        .Append("<a class='alert-link' ")
                        .Append($"href='{Url.Action("ProductDetails", "Products", new { id = productId })}'>")
                        .Append($"{product.Name}</a> добавлен <a class='alert-link' ")
                        .Append($"href='{Url.Action("Index", new { returnUrl })}'>в корзину покупок!</a>")
                        .ToString();
                } // if
            } // if

            return RedirectToAction("Index", "Products");
        }

        public IActionResult EditProductQuantity(int cartItemId, int quantity, string returnUrl)
        {
            CartItem cartItem = cart.Items.FirstOrDefault(ci => ci.Id == cartItemId);

            if (quantity > cartItem?.Product.Quantity) {
                TempData["message"] = $"Максимально можно заказать <b>{cartItem.Product.Quantity} шт.</b>";
                TempData["alertContextClass"] = "alert-danger";
            } else {
                cart.EditItemQuantity(cartItemId, quantity);
            } // if

            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = context.Products.Find(productId);

            if (product != null) cart.RemoveItem(product);

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}