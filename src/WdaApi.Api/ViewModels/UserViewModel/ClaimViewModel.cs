using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.ViewModels.UserViewModel
{
    public class ClaimViewModel
    {
        public ClaimViewModel()
        {
        }

        public ClaimViewModel(string value, string type)
        {
            Value = value;
            Type = type;
        }

        public string Value { get; set; }
        public string Type { get; set; }
    }
}
