using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SaturnApi.Business.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
        Task SendEmailAsync(string email, string subject, string message);

        Task SendEmailWithAttachmentAsync(string email, string subject, string message, Attachment attachment);
    }
}
