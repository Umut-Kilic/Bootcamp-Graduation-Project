﻿using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BootcampApp.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task SendResetPasswordEmailAsync(string resetPaswwordEmailLink, string ToEmail)
        {
            var smtpClient = new SmtpClient();

            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);

            mailMessage.Subject = "Şikayetmiz Var Şifre Sıfırlama Linki";
            mailMessage.Body = @$"
                <h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4>
                <p><a href='{resetPaswwordEmailLink}'>Şifre yenileme linki</a></p>";

            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
