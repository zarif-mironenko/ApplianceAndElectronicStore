using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApplianceAndElectronicStore.Models.Entities {

    public class Order
    {
        [BindNever]
        [HiddenInput]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [BindNever]
        [Range(1, int.MaxValue, ErrorMessage = "Не указан клиент")]
        [Display(Name = "Клиент")]
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        [BindNever]
        [Required(ErrorMessage = "Не указаны дата и время размещения")]
        [Display(Name = "Дата/время размещения")]
        public DateTime PlacementDateTime { get; set; }

        [BindNever]
        [Display(Name = "Общая стоимость")]
        [DisplayFormat(DataFormatString = "{0:N0} руб.")]
        public int TotalSum { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        //[BindNever]
        [Display(Name = "Отгружен?")]
        public bool Shipped { get; set; }

        [Required]
        [Display(Name = "Способ доставки")]
        public string DeliveryMethod { get; set; }

        [Required]
        [Display(Name = "Способ оплаты")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Не указан адрес")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Не указан город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Не указана область")]
        [Display(Name = "Область")]
        public string Region { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
