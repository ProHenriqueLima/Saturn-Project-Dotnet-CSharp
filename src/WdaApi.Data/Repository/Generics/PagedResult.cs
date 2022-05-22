using System;
using System.Collections.Generic;
using System.Text;

namespace WdaApi.Data.Repository
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
