using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
namespace RstateAPI.EmailServices {
    public class EmailSender : IEmailSender {

        public EmailOptions options { get; set; }
        public EmailSender (IOptions<EmailOptions> emailOpt) {
            options = emailOpt.Value;
        }
        public Task SendEmailAsync (string email, string subject, string htmlMessage) {
            return Execute (options.SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute (string sendGridKey, string subject, string htmlMessage, string email) {
            var clients = new SendGridClient (sendGridKey);
            var msg = new SendGridMessage {
                From = new EmailAddress ("bhargavajax@gmail.com", "Spacing"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo (new EmailAddress (email));
            try {
                return clients.SendEmailAsync (msg);
            } catch (Exception) { 
            }
            return null;
        }

    }
}