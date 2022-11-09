using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BLL.Services
{
    public sealed class EmailSenderService : IEmailSenderService
    {
        private readonly string _fromEmail;
        private readonly string _fromEmailPassword;


        public EmailSenderService()
        {
            _fromEmail = "SERHII.VENHRYN@lnu.edu.ua";
            _fromEmailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD", EnvironmentVariableTarget.User);
        }


        public async Task SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("CommentsWebApi", _fromEmail));
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
                    await client.AuthenticateAsync(_fromEmail, _fromEmailPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
