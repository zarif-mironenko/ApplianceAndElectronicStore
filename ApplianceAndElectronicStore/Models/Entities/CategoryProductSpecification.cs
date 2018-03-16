using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class CategoryProductSpecification
    {
        [HiddenInput]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [HiddenInput]
        public int ProductSpecificationId { get; set; }

        public ProductSpecification ProductSpecification { get; set; }
    }
}
