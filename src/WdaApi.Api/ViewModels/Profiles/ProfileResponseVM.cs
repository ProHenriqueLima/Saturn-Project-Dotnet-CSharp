using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.ViewModels
{
    public class ProfileResponseVM
    {
        public ProfileResponseVM()
        {
        }

        public ProfileResponseVM(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;           
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
