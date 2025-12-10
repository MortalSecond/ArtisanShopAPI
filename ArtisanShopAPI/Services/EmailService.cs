using ArtisanShopAPI.Models;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace ArtisanShopAPI.Services
{
    public interface IEmailService
    {
        Task SendContactEmailAsync(ContactInquiry inquiry);
        Task SendCommissionRequestEmailAsync(CommissionRequest request);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IFormattingService _formattingService;
        public EmailService(IConfiguration configuration, IFormattingService formattingService)
        {
            _configuration = configuration;
            _formattingService = formattingService;
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

        public async Task SendCommissionRequestEmailAsync(CommissionRequest request)
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
            message.Subject = $"New Commission Request: {request.Name} - {request.EstimatedPrice}";

            // Body
            string featuresList = _formattingService.BuildFeatureList(request.Features);
            string treatmentsList = _formattingService.BuildTreatmentList(request.Treatments);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #d4af37; border-bottom: 2px solid #d4af37; padding-bottom: 10px;'>
                        New Commission Request
                    </h2>
                
                    <div style='background-color: #f5f5f5; padding: 20px; margin: 20px 0; border-left: 4px solid #d4af37;'>
                        <h3 style='margin-top: 0;'>Customer Information</h3>
                        <p><strong>Name:</strong> {request.Name}</p>
                        <p><strong>Email:</strong> <a href='mailto:{request.Email}'>{request.Email}</a></p>
                        <p><strong>Phone:</strong> {request.Phone ?? "Not provided"}</p>
                        <p><strong>Submitted:</strong> {request.SubmittedAt:yyyy-MM-dd HH:mm:ss} UTC</p>
                    </div>

                    <div style='background-color: #fff9e6; padding: 20px; margin: 20px 0; border: 1px solid #d4af37;'>
                        <h3 style='color: #d4af37; margin-top: 0;'>Commission Configuration</h3>
                    
                        <table style='width: 100%; border-collapse: collapse;'>
                            <tr style='border-bottom: 1px solid #ddd;'>
                                <td style='padding: 10px; font-weight: bold;'>Size:</td>
                                <td style='padding: 10px;'>{_formattingService.FormatSize(request.Size)}</td>
                            </tr>
                            <tr style='border-bottom: 1px solid #ddd;'>
                                <td style='padding: 10px; font-weight: bold;'>Stone Coverage:</td>
                                <td style='padding: 10px;'>{_formattingService.FormatStoneCoverage(request.StoneCoverage)}</td>
                            </tr>
                            <tr style='border-bottom: 1px solid #ddd;'>
                                <td style='padding: 10px; font-weight: bold;'>Frame:</td>
                                <td style='padding: 10px;'>{_formattingService.FormatFrame(request.Frame)}</td>
                            </tr>
                            <tr style='border-bottom: 1px solid #ddd;'>
                                <td style='padding: 10px; font-weight: bold;'>Special Features:</td>
                                <td style='padding: 10px;'>
                                    {(string.IsNullOrEmpty(request.Features) ? "None" : featuresList)}
                                </td>
                            </tr>
                            <tr style='border-bottom: 1px solid #ddd;'>
                                <td style='padding: 10px; font-weight: bold;'>Surface Treatments:</td>
                                <td style='padding: 10px;'>
                                    {(string.IsNullOrEmpty(request.Treatments) ? "None" : treatmentsList)}
                                </td>
                            </tr>
                            <tr style='border-bottom: 1px solid #ddd;'>
                                <td style='padding: 10px; font-weight: bold;'>Shipping:</td>
                                <td style='padding: 10px;'>{_formattingService.FormatShipping(request.Shipping)}</td>
                            </tr>
                            <tr style='background-color: #d4af37; color: white;'>
                                <td style='padding: 15px; font-weight: bold; font-size: 18px;'>Estimated Total:</td>
                                <td style='padding: 15px; font-size: 18px; font-weight: bold;'>${request.EstimatedPrice:N2}</td>
                            </tr>
                        </table>
                    </div>

                    {(string.IsNullOrEmpty(request.Message) ? "" : $@"
                    <div style='background-color: #f9f9f9; padding: 20px; margin: 20px 0;'>
                        <h3>Customer Message</h3>
                        <p style='white-space: pre-wrap;'>{request.Message}</p>
                    </div>
                    ")}

                    {(string.IsNullOrEmpty(request.ImageUrl) ? "" : $@"
                    <div style='background-color: #f9f9f9; padding: 20px; margin: 20px 0;'>
                        <h3>Attached Painting Image</h3>
                        <img src='{request.ImageUrl}' style='max-width: 100%; height: auto; border: 2px solid #d4af37;' />
                        <p><a href='{request.ImageUrl}'>View Full Size</a></p>
                    </div>
                    ")}

                    <hr style='border: none; border-top: 1px solid #ddd; margin: 30px 0;'>
                </div>
                ";
            message.Body = bodyBuilder.ToMessageBody();

            // Add Reply-To
            message.ReplyTo.Add(new MailboxAddress(request.Name, request.Email));

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
