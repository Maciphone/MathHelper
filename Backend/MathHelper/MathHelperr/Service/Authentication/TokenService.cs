using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace SolarWatch.Service.Authentication;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const int ExpirationMinutes = 30;

    public string CreateToken(IdentityUser user, string role)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user, role),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
    {
         var jwtStettings =_configuration.GetSection("JWTSettings"); //IConfigurationSection object - kulcsokra hivatkozva get value like a json
         var validIssuer = jwtStettings["ValidIssuer"];
         var validAudience = jwtStettings["ValidAudience"];
        //var validIssuer = "apiWithBackend";
        //var validAudience = "apiWithBackend";
        return new JwtSecurityToken(
            validIssuer,
            validAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
    }

    private List<Claim> CreateClaims(IdentityUser user, string? role)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                    ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };
            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



    private SigningCredentials CreateSigningCredentials()
    {
        var secret = _configuration["IssuerSigningKey"];
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }

}

// private List<Claim> CreateClaims(IdentityUser user, string? role)
// {
//     try
//     {
//         var claims = new List<Claim>
//         {
//             new(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
//             new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//             new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
//             new(ClaimTypes.NameIdentifier, user.Id),
//             new(ClaimTypes.Name, user.UserName),
//             new(ClaimTypes.Email, user.Email),
//         };
//
//         if (role != null)
//         {
//             claims.Add(new Claim(ClaimTypes.Role, role));
//         }
//
//         return claims;
//     }
//     catch (Exception e)
//     {
//         Console.WriteLine(e);
//         throw;
//     }
// }
//