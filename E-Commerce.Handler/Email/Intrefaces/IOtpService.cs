public interface IOtpService
{
    string GenerateOtp();
    Task<bool> SendVerificationEmailAsync(string email, string otpCode);

    // Helper method to check if OTP is valid and not expired
    bool IsOtpValid(string providedOtp);

    // Helper method to clear OTP after successful verification
    void ClearOtp();
    
}