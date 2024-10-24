using MathHelperr.Service;
using MathHelperr.Service.ImplementationChecker;
using Microsoft.AspNetCore.Mvc;

namespace MathHelperr.Controller;

[ApiController]
[Route("api/level")]
public class LevelController : ControllerBase
{
    
    [HttpGet("{operation}")]
    public IActionResult GetNumberOfLevels(string operation)
    {
        Console.WriteLine(operation);
        int numberOfLevels;

        switch (operation.ToLower())
        {
            case "multiplication":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IMultiplicationTextGenerator>();
                break;
            case "algebra":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IAlgebraTextGenerator>();
                break;
            case "division":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IDivisionTextGenerator>();
                break;
            case "remaindivision":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IRemainDivisionTextGenerator>();
                break;
            default:
                return BadRequest("Unknown operation.");
        }
       // Console.WriteLine($"levels implemented: {numberOfLevels}");
        return Ok(numberOfLevels);
    }
    
}