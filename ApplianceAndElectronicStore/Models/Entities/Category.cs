using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class Category
    {
        [BindNever]
        [HiddenInput]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название категории")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите имя категории для ссылки")]
        [Display(Name = "Имя для ссылки")]
        public string NameForUrl { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductSpecificationNumberInOrder> ProductSpecificationNumbersInOrder { get; set; }

        public List<CategoryProductSpecification> CategoriesProductSpecifications { get; set; }

        public Category()
        {
            Products = new List<Product>();
            ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder>();
            CategoriesProductSpecifications = new List<CategoryProductSpecification>();
        }
    }
}
