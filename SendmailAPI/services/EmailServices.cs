using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SendmailAPI.Interfaces;
using SendmailAPI.Models;

namespace SendmailAPI.services
{
    public class EmailServices : IMailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailReceiver emailReceiver)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("agnes.pouros@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(emailReceiver.To));
            email.Subject = emailReceiver.Subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = emailReceiver.Body};
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
