using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [EmailAddress(ErrorMessage = "{0} введен неправильно")]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [StringLength(100, ErrorMessage = "{0} должен быть длиной не менее {2} и не более {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
