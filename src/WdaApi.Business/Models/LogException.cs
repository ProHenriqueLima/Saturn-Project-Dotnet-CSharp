using System;
using System.Collections.Generic;
using System.Text;

namespace SaturnApi.Business.Models
{
    public class LogException : Entity
    {
        public DateTime TimeStamp { get; set; }

        public string RequestId { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public string RequestPath { get; set; }

        public string User { get; set; }

        public string IpAddress { get; set; }
    }
}
