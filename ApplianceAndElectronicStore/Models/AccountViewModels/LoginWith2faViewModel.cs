using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [StringLength(7, ErrorMessage = "{0} должен быть длиной не менее {2} и не более {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Код аутентификатора")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Запомнить это устройство")]
        public bool RememberMachine { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
