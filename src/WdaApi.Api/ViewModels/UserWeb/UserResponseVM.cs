using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.ViewModels
{
    public class UserResponseVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public ProfileUserVM Profile { get; set; }
    }
}
