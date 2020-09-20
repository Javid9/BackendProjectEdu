using EducationBackendFinal.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationBackendFinal.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(List<string> email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Education Saytin Admini", "javiddadashov8@mail.ru"));
            foreach (var item in email)
            {
                emailMessage.To.Add(new MailboxAddress("", item));
            }
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync("javiddadashov8@mail.ru", "IyoKtdI3El1-");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
