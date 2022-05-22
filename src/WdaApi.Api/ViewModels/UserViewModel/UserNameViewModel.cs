using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.ViewModels.UserViewModel
{
    public class UserNameViewModel
    {
        [StringLength(120, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        public string Email { get; set; }
    }
}
