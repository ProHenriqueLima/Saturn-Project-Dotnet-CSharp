using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace SaturnApi.Business.Models
{
    public class ProfileUser : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
