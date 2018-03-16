using ApplianceAndElectronicStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.ProductsViewModels
{
    public class ManufacturerViewModel
    {
        public Manufacturer Manufacturer { get; set; }
        public bool IsSelected { get; set; }
    }
}
