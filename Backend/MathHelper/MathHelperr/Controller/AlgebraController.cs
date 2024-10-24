using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using MathHelperr.Data;
using MathHelperr.Model;
using MathHelperr.Service;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Groq;
using MathHelperr.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MathHelperr.Controller;
[ApiController]
[Route("api/algebra")]
public class AlgebraController :ControllerBase
{
  
    private readonly IGroqResultGenerator _groqResultGenerator;
    private readonly ApplicationDbContext _context;
    private readonly ICreatorRepository _creatorRepository;


    private readonly IMathFactory _mathFactory;
    public AlgebraController(IMathFactory mathFactory, IGroqResultGenerator groqResultGenerator, ApplicationDbContext context, ICreatorRepository creatorRepository)
    {
        _mathFactory = mathFactory;
        _groqResultGenerator = groqResultGenerator;
        _context = context;
        _creatorRepository = creatorRepository;
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
    
    
    [Authorize(Roles = "User")]
    [HttpGet("TestForDatabase") ]
    //[FromHeader(Name= "Level")]string level, 
    public async Task<ActionResult<ExcerciseResult>> TestDb(MathTypeName type)
    {
        var jwtToken = Request.Cookies["jwt"];
        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine(jwtToken);
        if (tokenHandler.CanReadToken(jwtToken))
        {
            var a_jwtToken = tokenHandler.ReadJwtToken(jwtToken);
            foreach (var claim in a_jwtToken.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
        }
        else
        {
            Console.WriteLine("Invalid token");
        }
       
        //frontend a headers-ben "Level" kulcson küldi
        Console.WriteLine("bent vagyok");
        var level = Request.Headers["Level"].ToString();
        Console.WriteLine($"level: {level}");
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        userId = "31e1ab6f-3175-49b6-9c19-879f8de77004";
        var user = User.Identity.Name;
        user = "Reka";
        Console.WriteLine($"username: {user}");
        Console.WriteLine($"userID: {userId}");
        

        if (userId == null)
        {
            return BadRequest("no id");
        }
      
        
        var exercise =_mathFactory.getMathExcercise(type);
        var solution = await _creatorRepository.GetSolution(exercise, type, level, userId);

        if(solution==null){Console.WriteLine("db crasch");}
        var question = solution.Exercise.Question;
        var answer = solution.Exercise.Result.ResultValues;
       
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer,
            Solution = solution
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