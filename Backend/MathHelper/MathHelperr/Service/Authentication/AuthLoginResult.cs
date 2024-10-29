namespace MathHelperr.Service.Authentication;

public record AuthLoginResult(
    bool Success,
    string Token,
    CookieOptions Options,
    string Email,
    string UserName)
{
//Error code - error message
public readonly Dictionary<string, string> ErrorMessages = new();
}
    
