using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Core.Models;
using Core.Structures;
using Infrastructure.Abstract;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Helpers;

public class JwtProvider : IJwtProvider
{
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value; 
    }
    
    private readonly JwtOptions _options;
    
    public string GenerateToken(User user)
    {
        Roles[] roles = user.Roles.ToArray();
        
        string rolesString = roles.ToString();
        
        Claim[] claims =
        {
            new Claim(nameof(user.Id), user.Id.ToString()),
        };

        byte[] secretKeyBytes = Encoding.UTF8.GetBytes(_options.SecretKey);
        SymmetricSecurityKey key = new SymmetricSecurityKey(secretKeyBytes);
        
        SigningCredentials singingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        DateTime timeExpires = DateTime.UtcNow.AddHours(_options.ExpiresHours);
        
        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: singingCredentials,
            expires: timeExpires
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        
        string tokenValue = handler.WriteToken(token);
        return tokenValue;
    }

    public string GetIdFromClaims(string jwtToken)
    {
        byte[] secretKeyBytes = Encoding.UTF8.GetBytes(_options.SecretKey);
        
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        
        TokenValidationParameters validations = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        ClaimsPrincipal claims = handler.ValidateToken(jwtToken, validations, out var tokenSecure);

        return claims.FindFirst(x => x.Type == nameof(User.Id))!.Value;
    }
}