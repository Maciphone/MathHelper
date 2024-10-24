using MathHelperr.Data;
using MathHelperr.Model;
using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Utility;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Service;

public class CreatorRepository : ICreatorRepository
{
    private readonly ApplicationDbContext _context;

    public CreatorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Solution> GetSolution(IMathExcercise mathExcercise, MathTypeName mathTypeName, string level, string userId )
    {
        //solutionId, resultId, exerciseId
        Result result = await GetResult(mathExcercise.Answer());
        Exercise exercise = await GetExercise(mathExcercise.Question(), mathTypeName, level, result.ResultId);
        Solution solution = await GetSolution(result, exercise, userId);
        
        return solution;
    }

    private async Task<Solution> GetSolution(Result result, Exercise exercise, string userId)
    {
        var solution = new Solution
        {
            CreatedAt = DateTime.Now,
            ExerciseId = exercise.ExerciseId,
            UserId = userId
        };
        _context.Solutions.Add(solution);
        await _context.SaveChangesAsync();
        return solution;
    }

    private async Task<Exercise> GetExercise(string question, MathTypeName mathTypeName, string level, int resultResultId)
    {
 
        var result = await _context.Exercises.FirstOrDefaultAsync(e => e.Question == question);
        if (result != null)
        {
            return result;
        }
        else
        {
            var exercise = new Exercise
            {
                Question = question,
                Level = level,
                MathType =  mathTypeName.ToString(),
                ResultId = resultResultId
            };
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }
    }

    private async Task<Result> GetResult(AlgebraResult answer)
    {
        int answerHash = CalculateHash(answer.Result);
        var result = await _context.Results.FirstOrDefaultAsync(r => r.ResultHash == answerHash);
        if (result != null)
        {
            return result;
        }
        else
        {
            var newResult = new Result
            {
                ResultHash = answerHash,
                ResultValues = answer.Result
            };
            _context.Results.Add(newResult);
            await _context.SaveChangesAsync();
            return newResult;
        }
    }
    
    public int CalculateHash(List<int> resultValues)
    {
        return resultValues.Aggregate(0, (acc, val) => acc + val.GetHashCode());
    }
}