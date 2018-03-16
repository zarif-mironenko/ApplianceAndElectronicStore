using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class ProductSpecification
    {
        //[BindNever]
        [HiddenInput]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название характеристики")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public List<ProductSpecificationNumberInOrder> ProductSpecificationNumbersInOrder { get; set; }

        public List<ProductSpecificationValue> ProductSpecificationValues { get; set; }

        public List<CategoryProductSpecification> CategoriesProductSpecifications { get; set; }

        public ProductSpecification()
        {
            ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder>();
            ProductSpecificationValues = new List<ProductSpecificationValue>();
            CategoriesProductSpecifications = new List<CategoryProductSpecification>();
        }
    }
}
