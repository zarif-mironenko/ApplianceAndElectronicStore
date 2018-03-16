using ApplianceAndElectronicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int ProductQuantity { get; set; }
    }
}
