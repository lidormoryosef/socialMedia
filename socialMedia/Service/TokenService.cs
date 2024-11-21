using System.Security.Claims;

namespace socialMedia.Service;

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class TokenService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly double _expirationMinutes;

    public TokenService(string secretKey, string issuer, string audience, double expirationMinutes)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
        _expirationMinutes = expirationMinutes;
    }

    public string GenerateToken(string username )
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_expirationMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    private ClaimsPrincipal? GetPrincipalFromToken(string token) {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var tokenHandler = new JwtSecurityTokenHandler();
        try {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,  
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = key    
            }, out SecurityToken validatedToken);
            return principal;
        }
        catch (Exception ex) {
            return null;
        }
    }

    public string? ExtractUsernameFromToken(string token)
    {
        var principal = GetPrincipalFromToken(token);
        if (principal is not null && principal.Identity is not null ) 
            return principal.Identity.Name;;
        return null;
    }

}
