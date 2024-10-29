using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using MathHelperr.Data;
using MathHelperr.Model;
using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Groq;
using MathHelperr.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MathHelperr.Controller;
[ApiController]
[Route("api/algebra")]
public class AlgebraController :ControllerBase
{
  
    private readonly IGroqResultGenerator _groqResultGenerator;
    private readonly ApplicationDbContext _context;
    private readonly ICreatorRepository _creatorRepository;
    private readonly IRepository<Solution> _solutionRepository;
    private readonly IMathFactory _mathFactory;
    
    public AlgebraController(
        IMathFactory mathFactory, 
        IGroqResultGenerator groqResultGenerator, 
        ApplicationDbContext context, 
        ICreatorRepository creatorRepository, 
        IRepository<Solution> solutionRepository)
    {
        _mathFactory = mathFactory;
        _groqResultGenerator = groqResultGenerator;
        _context = context;
        _creatorRepository = creatorRepository;
        _solutionRepository = solutionRepository;
      
    }

    [HttpGet("GetExercise")]
    public ActionResult<ExcerciseResult> GetQuestion(string type)
    {
        Console.WriteLine("bent vagyok!!!!");
        var typeCasted = (MathTypeName)Enum.Parse(typeof(MathTypeName), type); //parse string to MAthTypeName enum
        Console.WriteLine(typeCasted);
        //var exercise = _serviceProvider.GetRequiredService<IMathFactory>().getMathExcercise(type);
        var exercise =_mathFactory.getMathExcercise(typeCasted);
        var question = exercise.Question();
        var answer = exercise.Answer().Result;
        Console.WriteLine(answer.Count());
        answer.ForEach(a =>Console.WriteLine(a));
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer
        };
       
        return Ok(result);

    }

   
    [HttpPost("test")]
    public int JustTest(SolutionSolvedDto dto)
    {
        Console.WriteLine(dto);

        //var res = await _context.Solutions.FirstOrDefaultAsync(e => e.SolutionId == dto.SolutionId);
        return 1;
    }

    //[Authorize (Roles = "User")]
    [HttpPost("UpdateSolution")]
    public async Task<IActionResult> UpdateSolution(SolutionSolvedDto solutionSolvedDto)
    { 
      
        try
        {
            
            var solution =  await _context.Solutions.FirstOrDefaultAsync(s => s.SolutionId == solutionSolvedDto.SolutionId);
           if (solution == null)
           {
               return NotFound("no solution with this id");
           }

           var el = solutionSolvedDto.ElapsedTime;
           DateTime date = solutionSolvedDto.SolvedAt;
           
           solution.ElapsedTime = solutionSolvedDto.ElapsedTime;
           solution.SolvedAt = solutionSolvedDto.SolvedAt;
           Console.WriteLine(solution);
           await _solutionRepository.UpdateAsync(solution);
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
    
    
    [Authorize(Roles = "User")]
    [HttpGet("TestForDatabase") ]
    //[FromHeader(Name= "Level")]string level, 
    public async Task<ActionResult<ExcerciseResult>> TestDb(MathTypeName type)
    {
  
        //frontend a headers-ben "Level" kulcson küldi
      
        var level = Request.Headers["Level"].ToString();
        Console.WriteLine($"level: {level}");
        //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = User.FindAll(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
        foreach (var claim in User.FindAll(c=>c.Type==ClaimTypes.NameIdentifier))
        {
            Console.WriteLine(claim.Type, claim.Value);
        }
        var user = User.Identity.Name;
        
        Console.WriteLine($"username: {user}");
        Console.WriteLine($"userID: {userId}");
        

        if (userId == null)
        {
            return BadRequest("no id");
        }
      
        
        var exercise =_mathFactory.getMathExcercise(type);
        exercise.Answer().Result.ForEach(e=>Console.WriteLine($"origi eredmények: {e}"));
        var solution = await _creatorRepository.GetSolution(exercise, type, level, userId);

        if (solution == null)
        {
            return BadRequest();
        }
        var question = solution.Exercise.Question;
        var answer = solution.Exercise.Result.ResultValues;
        SolutionSolvedDto dto = new SolutionSolvedDto
        {
            SolutionId = solution.SolutionId
        };
       
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer,
            SolutionSolvedDto = dto
        };
       
        return Ok(result);

    }
    
    /*
    [HttpGet("GetAiExercise")]
    public async Task<ActionResult<ExcerciseResult>> GetAiExercies(MathTypeName type)
    {
        var exercise =_mathFactory.getMathExcercise(type);
        var question = exercise.Question();
        var answer = exercise.Answer().Result;
        var aiStringResponse = await _groqResultGenerator.GetAiText(question);
        
        var result = new ExcerciseResult()
        {
            Question = aiStringResponse,
            Result = answer
        };
       
        return Ok(result);

    }
    
    [HttpGet("GetAiExerciseTest")]
    public async Task<ActionResult<ExcerciseResult>> GetAiExerciseTest(MathTypeName type)
    {
        var exercise =_mathFactory.getMathExcercise(type);
        var aiStringResponse = await _groqResultGenerator.GetAiText(exercise.Question());
        var answer = exercise.Answer().Result;
        var result = new ExcerciseResult()
        {
            Question = aiStringResponse,
            Result = answer
        };
       
        return Ok(result);

    }
*/
    

}