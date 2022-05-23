using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.ViewModels
{
    public class FilterUserVM : FilterPagedVM
    {
        public Guid Id { get; set; }

        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        [Display(Name = "Status")]
        public bool? Status { get; set; }
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Display(Name = "ProfileName")]
        public string ProfileName { get; set; }
    }
}
