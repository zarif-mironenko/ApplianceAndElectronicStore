using System.Collections.Generic;
using ApplianceAndElectronicStore.Models.Entities;

namespace ApplianceAndElectronicStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public Category CurrentCategory { get; set; }
    }
}
