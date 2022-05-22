using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WdaApi.Business.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; } = false;
        public bool IsExcluded { get; set; } = false;        
        public Guid? ProfileId { get; set; }
        public ProfileUser Profile { get; set; }


    }
}
