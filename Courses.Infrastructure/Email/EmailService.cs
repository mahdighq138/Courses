using Courses.Application.Services.Email;
using Courses.Application.Services.ServiceInterfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Email
{
    public class EmailService(EmailSettings emailSettings) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettings;
        public async Task SendEmailAsync(string to, string subject, string body, 
            string toName = "", bool isHtml = false)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));

            message.Subject = subject;

            message.To.Add(new MailboxAddress(toName, to));

            if (isHtml)
            {
                message.Body = new TextPart("html")
                {
                    Text = body,
                };
            }
            else
            {
                message.Body = new TextPart()
                {
                    Text = body,
                };
            }

            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync(_emailSettings.SmtpHost,
                _emailSettings.SmtpPort,
                SecureSocketOptions.StartTls);

            await smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);

            await smtpClient.SendAsync(message);

            await smtpClient.DisconnectAsync(true);
        }
    }
}
