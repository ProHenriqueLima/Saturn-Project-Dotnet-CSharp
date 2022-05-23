using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.ViewModels.UserViewModel
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public ProfileResponseVM Profile { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}
