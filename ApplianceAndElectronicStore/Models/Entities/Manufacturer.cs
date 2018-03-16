using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class Manufacturer
    {
        [HiddenInput]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название производителя")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }

        public Manufacturer()
        {
            Products = new List<Product>();
        }
    }
}
