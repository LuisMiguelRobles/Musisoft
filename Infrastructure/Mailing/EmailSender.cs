namespace Infrastructure.Mailing
{
    using Application.Interfaces;
    using Domain;
    using Microsoft.Extensions.Options;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        private SmtpClient smtpClient { get; }
        private readonly IOptions<EmailSettings> _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
            smtpClient = new SmtpClient
            {
                Host = _emailSettings.Value.Host,
                Port = _emailSettings.Value.Port,
                DeliveryMethod =  SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.Value.Email, _emailSettings.Value.Password),
                EnableSsl = _emailSettings.Value.EnableSsl

            };
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage(_emailSettings.Value.Email, email, subject, message) {IsBodyHtml = true};
            return smtpClient.SendMailAsync(mail);
            
        }
    }
}
