using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class ProductSpecificationNumberInOrder
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public int ProductSpecificationId { get; set; }

        public ProductSpecification ProductSpecification { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "Не указан номер по порядку")]
        [Display(Name = "Номер по порядку")]
        public int NumberInOrder { get; set; }
    }
}
