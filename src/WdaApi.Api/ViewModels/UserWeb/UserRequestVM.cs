using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.ViewModels
{
    public class UserRequestVM
    {
        public UserRequestVM()
        {
        }

        public UserRequestVM(string fullName, string email, Guid profileId, bool status)
        {
            FullName = fullName;
            Email = email;
            ProfileId = profileId;
            Status = status;
        }

        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        [Display(Name = "FullName")]
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        public string FullName { get; set; }
        [StringLength(120, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "ProfileId")]
        public Guid ProfileId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}
