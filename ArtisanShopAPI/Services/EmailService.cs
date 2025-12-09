using ArtisanShopAPI.Models;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace ArtisanShopAPI.Services
{
    public interface IEmailService
    {
        Task SendContactEmailAsync(ContactInquiry inquiry);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendContactEmailAsync(ContactInquiry inquiry)
        {
            var message = new MimeMessage();

            // From Address (Website's Email)
            message.From.Add(new MailboxAddress(
                "Artisan Store Website",
                _configuration["Email:FromAddress"]
            ));

            // To Address (Receiver Email)
            message.To.Add(new MailboxAddress(
                "Diego",
                _configuration["Email:ToAddress"]
            ));

            // Subject
            message.Subject = $"{inquiry.InquiryType} from {inquiry.Name}";

            // Body
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <h2>New Contact Form Submission</h2>
                <p><strong>From:</strong> {inquiry.Name}</p>
                <p><strong>Email:</strong> {inquiry.Email}</p>
                <p><strong>Phone:</strong> {inquiry.Phone ?? "Not provided"}</p>
                <p><strong>Inquiry Type:</strong> {inquiry.InquiryType}</p>
                <p><strong>Submitted:</strong> {inquiry.SubmittedAt:yyyy-MM-dd HH:mm:ss} UTC</p>
                <hr>
                <h3>Message:</h3>
                <p>{inquiry.Message.Replace("\n", "<br>")}</p>";
            message.Body = bodyBuilder.ToMessageBody();

            // Add Reply-To
            message.ReplyTo.Add(new MailboxAddress(inquiry.Name, inquiry.Email));

            // Send via SMTP
            using var client = new SmtpClient();

            await client.ConnectAsync(
                _configuration["Email:SmtpHost"],
                int.Parse(_configuration["Email:SmtpPort"]),
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                _configuration["Email:Username"],
                _configuration["Email:Password"]
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
