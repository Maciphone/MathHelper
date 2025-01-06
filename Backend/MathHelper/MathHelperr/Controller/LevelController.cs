using MathHelperr.Service;
using MathHelperr.Service.LevelChecker;
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
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IMultiplicationExampleGenerator>();
                break;
            case "algebra":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IAlgebraExampleGenerator>();
                break;
            case "division":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IDivisionExampleGenerator>();
                break;
            case "remaindivision":
                numberOfLevels = LevelChecker.GetNumberOfImplementations<IRemainDivisonExampleGenerator>();
                break;
            default:
                return BadRequest("Unknown operation.");
        }
       // Console.WriteLine($"levels implemented: {numberOfLevels}");
        return Ok(numberOfLevels);
    }
    
}