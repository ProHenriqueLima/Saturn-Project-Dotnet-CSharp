using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.ViewModels.UserViewModel
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }
}
