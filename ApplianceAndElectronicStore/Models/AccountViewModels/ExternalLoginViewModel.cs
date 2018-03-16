using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [EmailAddress(ErrorMessage = "Адрес электронной почты введен неправильно")]
        public string Email { get; set; }
    }
}
