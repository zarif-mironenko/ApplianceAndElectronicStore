using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [EmailAddress(ErrorMessage = "{0} введен неправильно")]
        [Display(Name = "Логин")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
