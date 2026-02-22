using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Service
{
    public class EmailService
    {
        private readonly string _senderEmail;
        private readonly string _appPassword;

        public EmailService()
        {
            _senderEmail = ConfigurationManager.AppSettings["GmailSender"];
            _appPassword = ConfigurationManager.AppSettings["GmailAppPassword"];
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Community Events!", _senderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                // Connect to Gmail SMTP
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate using App Password
                client.Authenticate(_senderEmail, _appPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}