using System.Security.Claims;
using MathHelperr.Data;
using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
}