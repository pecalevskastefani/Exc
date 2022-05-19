using Eshop.Domain;
using Eshop.Domain.Domain_models;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }
        public async Task SendEmailAsync(List<EmailMessage> allMails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            foreach (var item in allMails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                    Subject = item.Subject
                };

                emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = item.Content };

                emailMessage.To.Add(new MailboxAddress(item.MailTo, item.MailTo));

                messages.Add(emailMessage);
            }
            try
            {
                //treba smtp konekcija tuka da konfigurirame
                using(var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    //konektiranje na server
                    var socketOptions = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOptions);

                    //da se avtorizirame na server
                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }
                    //koga ke zavrsime so porakite
                    await smtp.DisconnectAsync(true); //disconnect na serverot


                }

            }
            catch(SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
