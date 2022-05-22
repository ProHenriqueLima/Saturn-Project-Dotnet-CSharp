using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdaApi.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiryHours { get; set; }
        public string Emitter { get; set; }
        public string ValidIn { get; set; }
    }
}
