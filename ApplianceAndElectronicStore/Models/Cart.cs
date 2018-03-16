using ApplianceAndElectronicStore.Extensions;
using ApplianceAndElectronicStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models
{
    public class Cart
    {
        List<CartItem> itemCollection = new List<CartItem>();
        const string KEY_CART = "cart";

        public List<CartItem> Items => itemCollection;

        [DisplayFormat(DataFormatString = "{0:N0} руб.")]
        public int TotalValue => itemCollection.Sum(e => e.Product.Price * e.ProductQuantity);

        Cart() { }

        public static Cart GetSessionInstance(HttpContext context)
        {
            ISession session = context.Session;

            if (session.GetJson<Cart>(KEY_CART) == null)
                session.SetJson(KEY_CART, new Cart());

            Cart cart = session.GetJson<Cart>(KEY_CART); 
            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public void AddItem(Product product, int quantity)
        {
            CartItem item = itemCollection.FirstOrDefault(ci => ci.Product.Id == product.Id);

            if (item == null) {
                itemCollection.Add(new CartItem {
                    Id = itemCollection.LastOrDefault()?.Id + 1 ?? 1,
                    Product = product,
                    ProductQuantity = quantity
                });
            } else {
                item.ProductQuantity += quantity;
            } // if

            Session.SetJson(KEY_CART, this);
        }

        public void EditItemQuantity(int cartItemId, int quantity)
        {
            CartItem item = itemCollection.FirstOrDefault(ci => ci.Id == cartItemId);

            if (item == null) return;

            item.ProductQuantity = quantity;
            Session.SetJson(KEY_CART, this);
        }

        public void RemoveItem(Product product)
        {
            itemCollection.RemoveAll(l => l.Product.Id == product.Id);

            Session.SetJson(KEY_CART, this);
        }

        public void Clear()
        {
            itemCollection.Clear();

            Session.Remove(KEY_CART);
        }
    }
}
