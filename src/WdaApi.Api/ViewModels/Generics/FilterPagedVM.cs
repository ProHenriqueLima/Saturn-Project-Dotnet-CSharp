using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.ViewModels
{
    public  class FilterPagedVM
    {      
        [Display(Name = "PageIndex")]
        public int PageIndex { get; set; } = 0;

        [Display(Name = "PageSize")]
        public int PageSize { get; set; } = 0;
    
    }
}
