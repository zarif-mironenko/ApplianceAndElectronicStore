using ApplianceAndElectronicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.ViewModels
{
    public class OrderChekoutViewModel
    {
        public Order Order { get; set; }
        public Cart Cart { get; set; }
    }
}
