using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplianceAndElectronicStore.Data;
using ApplianceAndElectronicStore.Models;
using ApplianceAndElectronicStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplianceAndElectronicStore.Controllers
{
    [Authorize(Roles = "user")]
    public class OrdersController : Controller
    {
        readonly Cart cart;
        readonly ApplicationDbContext context;
        readonly UserManager<ApplicationUser> userManager;

        public OrdersController(Cart cartService, ApplicationDbContext contextService,
                                UserManager<ApplicationUser> userManagerService)
        {
            cart = cartService;
            context = contextService;
            userManager = userManagerService;
        }

        public IActionResult Checkout(string returnUrl)
        {
            ViewBag.ReturnUrl = Url.LocalUrl(returnUrl);

            return View(new Order {
                DeliveryMethod = "Доставка грузовой машиной",
                PaymentMethod = "Оплата при доставке"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!cart.Items.Any()) ModelState.AddModelError("", "Извините, ваша корзина пуста!");

            if (!ModelState.IsValid) return View(order);

            try {
                foreach (var item in cart.Items) {
                    order.OrderItems.Add(new OrderItem {
                        NumberInOrder = item.Id,
                        ProductId = item.Product.Id,
                        ProductQuantity = item.ProductQuantity
                    });
                } // foreach

                var orderProductsIds = order.OrderItems
                    .Select(oi => oi.ProductId)
                    .ToList();

                var orderProducts = await context.Products
                    .Where(p => orderProductsIds.Contains(p.Id))
                    .ToListAsync();

                for (int i = 0; i < orderProducts.Count; i++) {
                    orderProducts[i].Quantity -= order.OrderItems[i].ProductQuantity;
                } // for i

                order.TotalSum = cart.TotalValue;
                order.CustomerId = userManager.GetUserId(User);
                order.PlacementDateTime = DateTime.Now;

                context.Orders.Add(order);
                context.Products.UpdateRange(orderProducts);

                await context.SaveChangesAsync();

                return RedirectToAction("Completed");
            } catch {
                return View();
            } // try-catch
        }

        public IActionResult Completed()
        {
            cart.Clear();

            return View();
        }
    }
}