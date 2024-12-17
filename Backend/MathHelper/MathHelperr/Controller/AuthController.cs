using MathHelperr.Service.Authentication;

using Microsoft.AspNetCore.Mvc;
using SolarWatch.Contracts;


namespace MathHelperr.Controller;

[ApiController]
[Route("api/authentication")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
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
        var tokenValidTimesSpan =int.Parse(_configuration["Jwt:CookieExpiration"]);
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

        var result = await _authService.LoginAsync(request.Email, request.Password, tokenValidTimesSpan);
        if (!result.Success)
        {
            AddErrors(result);
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            Console.WriteLine("Authentication failed:");
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            return BadRequest(errors);
        }
        var jwtKey = _configuration["CookieName"];
        
        //token to send in headers
        var token = result.Token;
        var cookieOptions = result.Options;
        Response.Cookies.Append(jwtKey, token, cookieOptions);

        return Ok(new AuthResponse(result.Email, result.UserName, tokenValidTimesSpan));
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }

}