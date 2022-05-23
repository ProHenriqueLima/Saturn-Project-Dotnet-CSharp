using SaturnApi.Business.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SaturnApi.Api.Extensions
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailSettings _emailSettings;
        public EmailSender(IEmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void SendEmail(string email, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(_emailSettings.From, email, subject, message);

            using (SmtpClient smtp = new SmtpClient(_emailSettings.SMTPAddress, Convert.ToInt32(_emailSettings.Port)))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                smtp.Send(mailMessage);
            }
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {           

            MailMessage mailMessage = new MailMessage(_emailSettings.From, email, subject, message);          

            using (SmtpClient smtp = new SmtpClient(_emailSettings.SMTPAddress, Convert.ToInt32(_emailSettings.Port)))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                await smtp.SendMailAsync(mailMessage);
            }
        }

        public async Task SendEmailWithAttachmentAsync(string email, string subject, string message, Attachment attachment)
        {

            MailMessage mailMessage = new MailMessage(_emailSettings.From, email, subject, message);

            mailMessage.Attachments.Add(attachment);

            using (SmtpClient smtp = new SmtpClient(_emailSettings.SMTPAddress, Convert.ToInt32(_emailSettings.Port)))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
