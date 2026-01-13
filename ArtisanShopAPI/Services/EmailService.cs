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
            try
            {
                Console.WriteLine("[EMAIL] Sending via Resend API");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["Resend:ApiKey"]}");

                var payload = new
                {
                    from = "Taller Geoda <onboarding@resend.dev>",
                    to = new[] { _configuration["Email:ToAddress"] },
                    reply_to = inquiry.Email,
                    subject = $"{inquiry.InquiryType} de {inquiry.Name}",
                    html = $@"
                        <h2>Nueva Pregunta</h2>
                        <p><strong>De:</strong> {inquiry.Name}</p>
                        <p><strong>Email:</strong> {inquiry.Email}</p>
                        <p><strong>Telefono:</strong> {inquiry.Phone ?? "Not provided"}</p>
                        <p><strong>Tipo de Pregunta:</strong> {inquiry.InquiryType}</p>
                        <p><strong>Fecha de Envío:</strong> {inquiry.SubmittedAt:yyyy-MM-dd HH:mm:ss} UTC</p>
                        <hr>
                        <h3>Mensaje:</h3>
                        <p>{inquiry.Message.Replace("\n", "<br>")}</p>"
                    };

                var response = await httpClient.PostAsJsonAsync("https://api.resend.com/emails", payload);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Resend API error: {error}");
                }

                Console.WriteLine("[EMAIL] Successfully sent via Resend");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL ERROR] {ex.Message}");
                throw;
            }
        }

        public async Task SendCommissionRequestEmailAsync(CommissionRequest request)
        {
            try
            {
                Console.WriteLine("[EMAIL] Sending via Resend API");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["Resend:ApiKey"]}");

                string featuresList = _formattingService.BuildFeatureList(request.Features);
                string treatmentsList = _formattingService.BuildTreatmentList(request.Treatments);
                var payload = new
                {
                    from = "Taller Geoda <onboarding@resend.dev>",
                    to = new[] { _configuration["Email:ToAddress"] },
                    reply_to = request.Email,
                    subject = $"Nueva Comisión: {request.Name} - {request.EstimatedPrice}",
                    html = $@"
                        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                        <h2 style='color: #d4af37; border-bottom: 2px solid #d4af37; padding-bottom: 10px;'>
                            Nueva Comisión
                        </h2>
                
                        <div style='background-color: #f5f5f5; padding: 20px; margin: 20px 0; border-left: 4px solid #d4af37;'>
                            <h3 style='margin-top: 0;'>Informacion del Cliente</h3>
                            <p><strong>Nombre:</strong> {request.Name}</p>
                            <p><strong>Email:</strong> <a href='mailto:{request.Email}'>{request.Email}</a></p>
                            <p><strong>Telefono:</strong> {request.Phone ?? "Not provided"}</p>
                            <p><strong>Fecha de Envío:</strong> {request.SubmittedAt:yyyy-MM-dd HH:mm:ss} UTC</p>
                        </div>

                        <div style='background-color: #fff9e6; padding: 20px; margin: 20px 0; border: 1px solid #d4af37;'>
                            <h3 style='color: #d4af37; margin-top: 0;'>Detalles de Comisión</h3>
                    
                            <table style='width: 100%; border-collapse: collapse;'>
                                <tr style='border-bottom: 1px solid #ddd;'>
                                    <td style='padding: 10px; font-weight: bold;'>Size:</td>
                                    <td style='padding: 10px;'>{_formattingService.FormatSize(request.Size)}</td>
                                </tr>
                                <tr style='border-bottom: 1px solid #ddd;'>
                                    <td style='padding: 10px; font-weight: bold;'>Piedras:</td>
                                    <td style='padding: 10px;'>{_formattingService.FormatStoneCoverage(request.StoneCoverage)}</td>
                                </tr>
                                <tr style='border-bottom: 1px solid #ddd;'>
                                    <td style='padding: 10px; font-weight: bold;'>Marco:</td>
                                    <td style='padding: 10px;'>{_formattingService.FormatFrame(request.Frame)}</td>
                                </tr>
                                <tr style='border-bottom: 1px solid #ddd;'>
                                    <td style='padding: 10px; font-weight: bold;'>Caracteristicas Especiales:</td>
                                    <td style='padding: 10px;'>
                                        {(string.IsNullOrEmpty(request.Features) ? "None" : featuresList)}
                                    </td>
                                </tr>
                                <tr style='border-bottom: 1px solid #ddd;'>
                                    <td style='padding: 10px; font-weight: bold;'>Tratamientos:</td>
                                    <td style='padding: 10px;'>
                                        {(string.IsNullOrEmpty(request.Treatments) ? "None" : treatmentsList)}
                                    </td>
                                </tr>
                                <tr style='border-bottom: 1px solid #ddd;'>
                                    <td style='padding: 10px; font-weight: bold;'>Envío:</td>
                                    <td style='padding: 10px;'>{_formattingService.FormatShipping(request.Shipping)}</td>
                                </tr>
                                <tr style='background-color: #d4af37; color: white;'>
                                    <td style='padding: 15px; font-weight: bold; font-size: 18px;'>Total Estimado:</td>
                                    <td style='padding: 15px; font-size: 18px; font-weight: bold;'>${request.EstimatedPrice:N2}</td>
                                </tr>
                            </table>
                        </div>

                        {(string.IsNullOrEmpty(request.Message) ? "" : $@"
                            <div style='background-color: #f9f9f9; padding: 20px; margin: 20px 0;'>
                                <h3>Mensaje Adjunto</h3>
                                <p style='white-space: pre-wrap;'>{request.Message}</p>
                            </div>
                        ")}

                        {(string.IsNullOrEmpty(request.ImageUrl) ? "" : $@"
                            <div style='background-color: #f9f9f9; padding: 20px; margin: 20px 0;'>
                                <h3>Imagen Adjunta</h3>
                                <img src='{request.ImageUrl}' style='max-width: 100%; height: auto; border: 2px solid #d4af37;' />
                                <p><a href='{request.ImageUrl}'>Ver Tamaño Completo</a></p>
                            </div>
                        ")}

                        <hr style='border: none; border-top: 1px solid #ddd; margin: 30px 0;'>
                        </div>
                        "
                };

                var response = await httpClient.PostAsJsonAsync("https://api.resend.com/emails", payload);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Resend API error: {error}");
                }

                Console.WriteLine("[EMAIL] Successfully sent via Resend");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL ERROR] {ex.Message}");
                throw;
            }
        }
    }
}
