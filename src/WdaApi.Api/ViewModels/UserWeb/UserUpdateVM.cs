using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.ViewModels
{
    public class UserUpdateVM
    {
        public string FullName { get; set; }    

        public Guid ProfileId { get; set; }

        public bool Status { get; set; } = false;
    }
}
