using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace WhoWantsToBeAMillionaireGame.EmailService
{
    public class EmailService
    {
        public void SendEmailAsync(string email, string subject, string message)
        {
            MimeMessage messageMimeMessage = new MimeMessage();
            messageMimeMessage.From.Add(new MailboxAddress("Oyna Qazan", "rotnet.az@yandex.ru"));
            messageMimeMessage.To.Add(new MailboxAddress("Oyna Qazan", email));
            messageMimeMessage.Subject = subject;
            messageMimeMessage.Body = new BodyBuilder() { HtmlBody = message }.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.yandex.ru", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("rotnet.az@yandex.ru", "ferhad67");
                client.Send(messageMimeMessage);
                client.Disconnect(true);
            }
        }
    }
}
