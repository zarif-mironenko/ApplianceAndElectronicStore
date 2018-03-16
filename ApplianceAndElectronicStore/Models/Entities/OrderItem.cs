using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class OrderItem
    {
        [BindNever]
        public int Id { get; set; }

        [Required]
        [HiddenInput]
        [Display(Name = "Номер п/п")]
        public int NumberInOrder { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        [HiddenInput]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [HiddenInput]
        public int ProductQuantity { get; set; }
    }
}
