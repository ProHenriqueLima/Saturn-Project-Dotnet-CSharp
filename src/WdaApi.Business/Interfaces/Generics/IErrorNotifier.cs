using WdaApi.Business.ErrorNotifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace WdaApi.Business.Interfaces
{
    public interface IErrorNotifier
    {
        bool HasErrorNotification();
        List<ErrorNotification> GetErrorNotifications();
        void Handle(ErrorNotification errorNotification);
    }
}
