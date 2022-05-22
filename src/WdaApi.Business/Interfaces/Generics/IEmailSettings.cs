using System;
using System.Collections.Generic;
using System.Text;

namespace WdaApi.Business.Interfaces
{
    public interface IEmailSettings
    {
        string From { get; }

        string User { get; }

        string Password { get; }

        string SMTPAddress { get; }

        string Port { get; }
    }
}
