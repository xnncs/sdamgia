using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Abstract;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    public PasswordVerificationResult VerifyHashedPassword(string providedPassword, string hashedPassword);
}