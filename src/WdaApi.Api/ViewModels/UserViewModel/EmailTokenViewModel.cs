using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.ViewModels.UserViewModel
{
    //lowercase initial characters at the request of the front, for automatic object conversion
    public class EmailTokenViewModel
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string token { get; set; }
    }
}
