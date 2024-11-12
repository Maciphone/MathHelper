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
using MathHelperr.Service.Repository;
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
    
    [Authorize(Roles = "User")]
    [HttpGet("TestForDatabase") ]
    //[FromHeader(Name= "Level")]string level, 
    public async Task<ActionResult<ExcerciseResult>> TestDb(MathTypeName type)
    {
  
        //frontend a headers-ben "Level" kulcson 
        var level = Request.Headers["Level"].ToString();
        //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = User.FindAll(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
        foreach (var claim in User.FindAll(c=>c.Type==ClaimTypes.NameIdentifier))
        {
            Console.WriteLine(claim.Type, claim.Value);
        }
        var user = User.Identity.Name;

        if (userId == null)
        {
            return BadRequest("no id");
        }
      
        
        var exercise =_mathFactory.getMathExcercise(type);
  //      exercise.Answer().Result.ForEach(e=>Console.WriteLine($"origi eredmények: {e}"));
 
        
        var question = exercise.Question();
        var answer = exercise.Answer().Result;
        var exerciseId = await _creatorRepository.GetExerciseId(exercise, type, level, userId, null);
       
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer,
           ExerciseId = exerciseId
        };
       
        return Ok(result);

    }
    
    
    [HttpGet("GetAiExercise")]
    public async Task<ActionResult<ExcerciseResult>> GetAiExercies(MathTypeName type)
    {
     
        var level = Request.Headers["Level"].ToString();
        var userId = User.FindAll(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
     

        if (userId == null)
        {
            return BadRequest("no id");
        }
      
        
        var exercise =_mathFactory.getMathExcercise(type);
        
        var question =  await _groqResultGenerator.GetAiText(exercise.Question());
        var answer = exercise.Answer().Result;
        var exerciseId = await _creatorRepository.GetExerciseId(exercise, type, level, userId, question);
       
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer,
            ExerciseId = exerciseId
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

    

}