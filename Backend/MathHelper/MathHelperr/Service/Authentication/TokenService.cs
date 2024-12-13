using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MathHelperr.Service.Authentication;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string CreateToken(IdentityUser user, string role)
    {
        var expirationMinutes = int.Parse(_configuration["Jwt:CookieExpiration"]);
        var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);
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
         var jwtStettings =_configuration.GetSection("Jwt"); //IConfigurationSection object - kulcsokra hivatkozva get value like a json
         var validIssuer = jwtStettings["ValidIssuer"];
         var validAudience = jwtStettings["ValidAudience"];
 
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
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("ApiToken", "TokenForTheApiWithAuth"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
                    ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                
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
       // var secret = _configuration["Jwt:IssuerSigningKey"];
       var secret = Environment.GetEnvironmentVariable("JWT_IssuerSigningKey");
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