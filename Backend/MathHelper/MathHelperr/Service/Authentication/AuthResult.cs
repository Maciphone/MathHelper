namespace MathHelperr.Service.Authentication;

public record AuthResult(
    bool Success,
    string Email,
    string UserName,
    string Token,
    CookieOptions Options)
{
    //Error code - error message
    public readonly Dictionary<string, string> ErrorMessages = new();
}