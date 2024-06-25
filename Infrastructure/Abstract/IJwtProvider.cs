using Core.Models;

namespace Infrastructure.Abstract;

public interface IJwtProvider
{
    string GenerateToken(User user); 
    string GetIdFromClaims(string jwtToken);
}