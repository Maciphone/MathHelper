using Microsoft.AspNetCore.Identity;

namespace MathHelperr.Service.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);
}