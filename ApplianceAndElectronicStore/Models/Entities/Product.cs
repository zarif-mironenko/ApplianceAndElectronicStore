using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.Entities
{
    public class Product
    {
        string _name;

        [BindNever]
        [HiddenInput]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Укажите наименование")]
        [Display(Name = "Наименование")]
        public string Name
        {
            get {
                if (string.IsNullOrEmpty(_name) && Manufacturer != null)
                    _name = $"{Manufacturer.Name} {ProductModel}";

                return _name;
            }
            set {
                _name = !string.IsNullOrEmpty(value) ? value : Manufacturer != null ?
                         $"{Manufacturer.Name} {ProductModel}" : null;
            }
        }

        [Required(ErrorMessage = "Укажите модель")]
        [Display(Name = "Модель")]
        public string ProductModel { get; set; }

        [HiddenInput]
        [Display(Name = "Фото товара")]
        public string ImagePath { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Не указана категория")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Не указан производитель")]
        [Display(Name = "Производитель")]
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        [Required(ErrorMessage = "Укажите описание товара")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Укажите цену больше 0")]
        [Display(Name = "Цена")]
        [DisplayFormat(DataFormatString = "{0:N0} руб.")]
        public int Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Укажите количество не меньше 0")]
        [Display(Name = "Количество")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Quantity { get; set; }

        public List<ProductSpecificationValue> ProductSpecificationValues { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Product()
        {
            ProductSpecificationValues = new List<ProductSpecificationValue>();
            OrderItems = new List<OrderItem>();
        }
    }
}
