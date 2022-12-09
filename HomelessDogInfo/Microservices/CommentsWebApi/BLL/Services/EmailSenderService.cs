using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.POCOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BLL.Services
{
    public sealed class EmailSenderService : IEmailSenderService
    {
        private readonly IOptions<EmailData> _config;

        public EmailSenderService(IOptions<EmailData> config)
        {
            _config = config;
        }


        public async Task SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("CommentsWebApi", _config.Value.Address));
            emailMessage.To.Add(new MailboxAddress("", emailModel.ToEmail));
            emailMessage.Subject = emailModel.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = emailModel.Body;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_config.Value.Address, _config.Value.Password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch { throw; }
        }
    }
}
