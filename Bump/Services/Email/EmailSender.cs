using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Bump.Services.Email
{
    public class EmailSender
    {
        private readonly EmailSettings _settings;

        public EmailSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendWelcomeEmail(string to, string verify)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_settings.Mail),
                Body = new BodyBuilder
                {
                    TextBody = $"You have been registered to BUMP. " +
                               $"Please follow ${verify} to confirm your email."
                }.ToMessageBody()
            };
            email.To.Add(MailboxAddress.Parse(to));
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, false);
            await smtp.AuthenticateAsync(_settings.Mail, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}