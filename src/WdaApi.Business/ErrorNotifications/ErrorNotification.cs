using System;
using System.Collections.Generic;
using System.Text;

namespace SaturnApi.Business.ErrorNotifications
{
    public class ErrorNotification
    {
        public string Message { get; }

        public ErrorNotification(string message)
        {
            Message = message;
        }
    }
}
