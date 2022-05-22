using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.ViewModels
{
    public class ProfileUserVM
    {
        public Guid Id { get; set; }
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
