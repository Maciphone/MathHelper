using MathHelperr.Service.Authentication;

using Microsoft.AspNetCore.Mvc;
using SolarWatch.Contracts;


namespace MathHelperr.Controller;

[ApiController]
[Route("api/authentication")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.RegisterAsync(request.Email, request.Username, request.Password, "User");
        
        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            Console.WriteLine("ModelState is invalid:");
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            return BadRequest(ModelState);
        }
        Console.WriteLine(request.Email);
        Console.WriteLine(request.Password);

        var result = await _authService.LoginAsync(request.Email, request.Password);
        Console.WriteLine(result.UserName);
        
        //token to send in headers
        var token = result.Token;
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30),
            SameSite = SameSiteMode.None,
            Secure = true //only on Https con, else true
        };
        Response.Cookies.Append("jwt", token, cookieOptions);

        if (!result.Success)
        {
            AddErrors(result);
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            Console.WriteLine("Authentication failed:");
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            return BadRequest(ModelState);
        }

        return Ok(new AuthResponse(result.Email, result.UserName));
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }

}