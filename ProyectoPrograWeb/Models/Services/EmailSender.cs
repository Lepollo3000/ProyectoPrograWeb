using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProyectoPrograWeb.Models.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        public Task Execute(string strSubject, string strMessage, string strEmailTo)
        {

            MailMessage email = new MailMessage(new MailAddress(Options.From, "(do not reply)"),
            new MailAddress(strEmailTo));

            email.Subject = strSubject;
            email.Body = strMessage;

            email.IsBodyHtml = true;

            var mailClient = new GmailEmailService(Options.Server, Options.Port, Options.SSL, Options.Username, Options.Password);
            {
                //In order to use the original from email address, uncomment this line:
                //email.From = new MailAddress(mailClient.UserName, "(do not reply)");

                return mailClient.SendMailAsync(email);
            }



            /*
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Joe@contoso.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
            */
        }
    }
}
