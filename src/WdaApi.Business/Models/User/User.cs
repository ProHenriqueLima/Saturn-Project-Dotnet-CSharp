using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaturnApi.Business.Models
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }     
        public ApplicationUser UserIdentity { get; set; }
    }
}

