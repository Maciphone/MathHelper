using System.Net.Http.Headers;
using MathHelperr.Model;
using MathHelperr.Service;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Groq;
using Microsoft.AspNetCore.Mvc;

namespace MathHelperr.Controller;
[ApiController]
[Route("api/algebra")]
public class AlgebraController :ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly GroqApiClient _groqApiClient;
    private readonly GroqRequest _groqRequest;

    // public AlgebraController(IServiceProvider serviceProvider)
    // {
    //     _serviceProvider = serviceProvider;
    // }

    private readonly IMathFactory _mathFactory;
    public AlgebraController(IMathFactory mathFactory, GroqApiClient groqApiClient, GroqRequest groqRequest)
    {
        _mathFactory = mathFactory;
        _groqApiClient = groqApiClient;
        _groqRequest = groqRequest;
    }

    [HttpGet("GetExercise")]
    public ActionResult<ExcerciseResult> GetQuestion(string type)
    {
        Console.WriteLine(type);
        //var exercise = _serviceProvider.GetRequiredService<IMathFactory>().getMathExcercise(type);
        var exercise =_mathFactory.getMathExcercise(type);
        var question = exercise.Question();
        var answer = exercise.Answer().Result;
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer
        };
       
        return Ok(result);

    }
    
    [HttpGet("GetAiExercise")]
    public async Task<ActionResult<ExcerciseResult>> GetAiExercies(string type)
    {
        var exercise =_mathFactory.getMathExcercise(type);
        var question = exercise.Question();
        var request = _groqRequest.GetRequestData(question);
        var aiResponse = await _groqApiClient.CreateChatCompletionAsync(request);
        var aiStringResponse = aiResponse?["choices"]?[0]?["message"]?["content"]?.ToString();
        
        var answer = exercise.Answer().Result;
        var result = new ExcerciseResult()
        {
            Question = aiStringResponse,
            Result = answer
        };
       
        return Ok(result);

    }

    

}