using System;
using System.Collections.Generic;
using System.Text;

namespace WdaApi.Business.Dto
{
    public class FilterProfileUserDto : FilterGeneric
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
