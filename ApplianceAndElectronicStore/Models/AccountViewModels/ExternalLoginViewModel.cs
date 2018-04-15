using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "���� \"{0}\" �����������")]
        [EmailAddress(ErrorMessage = "����� ����������� ����� ������ �����������")]
        public string Email { get; set; }
    }
}
