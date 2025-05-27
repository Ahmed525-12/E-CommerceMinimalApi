using System.Security.Cryptography;
using E_Commerce.Handler.Email.Intrefaces;
using ElmnasaDomain.DTOs.EmailDTO;
using MailKit;
using Microsoft.Extensions.Logging;

public class OtpService : IOtpService
{
    private readonly IEmailSettings _emailService;
    private readonly ILogger<OtpService> _logger;

    // Properties to hold OTP code and expiration
    private string OtpCode { get; set; }
    private DateTime? OtpExpiresAt { get; set; }

    public OtpService(IEmailSettings emailService, ILogger<OtpService> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public string GenerateOtp()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);

        var number = Math.Abs(BitConverter.ToInt32(bytes, 0));
        return (number % 1000000).ToString("D6");
    }

    public async Task<bool> SendVerificationEmailAsync(string email, string otpCode)
    {
        try
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Verify Your Email Address",
                Body = $@"Your verification code is: {otpCode}."
            };

            await Task.Run(() => _emailService.SendEmail(emailDto)); // Simulating async behavior
            _logger.LogInformation("Verification email sent successfully to {Email}", email);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send verification email to {Email}", email);
            return false;
        }
    }
    

    // Helper method to check if OTP is valid and not expired
    public bool IsOtpValid(string providedOtp)
    {
        return !string.IsNullOrEmpty(OtpCode) &&
               OtpExpiresAt.HasValue &&
               OtpExpiresAt.Value > DateTime.UtcNow &&
               OtpCode == providedOtp;
    }

    // Helper method to clear OTP after successful verification
    public void ClearOtp()
    {
        OtpCode = null;
        OtpExpiresAt = null;
    }
}