using ApplianceAndElectronicStore.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.ProductsViewModels
{
    public class ProductSearchFiltersViewModel
    {
        public Dictionary<int, ProductSpecification> ProductSpecifications { get; set; }
        public Dictionary<int, Dictionary<int, ProductSpecificationValueViewModel>> ProdSpecsWithValues { get; set; }
        public Dictionary<int, ManufacturerViewModel> Manufacturers { get; set; }
        public int MinProdPrice { get; set; }
        public int MaxProdPrice { get; set; }
    }
}
