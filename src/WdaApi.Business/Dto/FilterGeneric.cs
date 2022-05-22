using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WdaApi.Business.Dto
{
    public abstract class FilterGeneric
    {
        [Display(Name = "PageIndex")]
        public int PageIndex { get; set; } = 0;

        [Display(Name = "PageSize")]
        public int PageSize { get; set; } = 0;
    }
}
