using System.Security.Claims;
using MathHelperr.Data;
using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Controller;

[ApiController]
[Route("api/solution")]
public class SolutionController :ControllerBase
{
    
    private readonly ApplicationDbContext _context;
    private readonly IRepository<Solution> _solutionRepository;
    private readonly IRepositoryUserData<SolutionDto> _repositorySolutionDto;
    

    public SolutionController(ApplicationDbContext context, IRepository<Solution> solutionRepository, IRepositoryUserData<SolutionDto> repositorySolutionDto)
    {
        _context = context;
        _solutionRepository = solutionRepository;
        _repositorySolutionDto = repositorySolutionDto;
    }
    [Authorize]
    [HttpGet("userSolutions")]
    public async Task<ActionResult<IEnumerable<SolutionDto>>> GetUserSolutions()
    {
        var userId = User.FindAll(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        if (userId == null)
        {
            return BadRequest("no such user");
        }

        try
        {
            var solutionDtos = await _repositorySolutionDto.GetAllByUserIdAsync(userId);
            return Ok(solutionDtos);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    //[Authorize (Roles = "User")]
    [HttpPost("UpdateSolution")]
    public async Task<IActionResult> CreateSolution(SolutionSolvedDto solutionSolvedDto)
    { 
      
        try
        {
            var userId = User.FindAll(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            var solution = new Solution
            {
                ExerciseId = solutionSolvedDto.ExerciseId,
                UserId = userId,
                ElapsedTime = solutionSolvedDto.ElapsedTime,
                SolvedAt = solutionSolvedDto.SolvedAt.ToLocalTime(),
                CreatedAt = DateTime.Now,
            };
        
            Console.WriteLine(solution);
            await _solutionRepository.AddAsync(solution);
            
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("A concurrency probléma lépett fel.");

        }
        catch (DbUpdateException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Adatbázis hiba történt.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Váratlan hiba történt: {ex.Message}");
        }

       

    }
}