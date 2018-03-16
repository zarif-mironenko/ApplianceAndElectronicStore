using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class ProductSpecificationValue
    {
        [HiddenInput]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [HiddenInput]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [HiddenInput]
        public int ProductSpecificationId { get; set; }

        //[Display(Name = "Характеристика")]
        public ProductSpecification ProductSpecification { get; set; }

        //[Required(ErrorMessage = "Не указано значение характеристики")]
        [Display(Name = "Значение")]
        public string Value { get; set; }
    }
}
