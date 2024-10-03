using MathHelperr.Model;
using MathHelperr.Service;
using MathHelperr.Service.Factory;
using Microsoft.AspNetCore.Mvc;

namespace MathHelperr.Controller;
[ApiController]
[Route("api/algebra")]
public class AlgebraController :ControllerBase
{
    private readonly IMathFactory _mathFactory;

    public AlgebraController(IMathFactory mathFactory)
    {
        _mathFactory = mathFactory;
    }

    [HttpGet("GetExercise")]
    public ActionResult<ExcerciseResult> GetQuestion(string type)
    {
        Console.WriteLine(type);
        var exercise = _mathFactory.getMathExcercise(type);
        var question = exercise.Question();
        var answer = exercise.Answer().Result;
        ExcerciseResult result = new ExcerciseResult()
        {
            Question = question,
            Result = answer
        };
       
        return Ok(result);

    }

    

}