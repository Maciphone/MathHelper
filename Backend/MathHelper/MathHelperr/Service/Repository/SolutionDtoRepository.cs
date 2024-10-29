

using MathHelperr.Data;
using MathHelperr.Model.Db.DTO;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Service;

public class SolutionDtoRepository : IRepositoryUserData<SolutionDto>
{
    private readonly ApplicationDbContext _context;
    
    public SolutionDtoRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<SolutionDto> GetByUserIdAsync(int id, string userId)
    {
        var s =  await _context.Solutions
            .Include(e =>e.Exercise)
            .ThenInclude(e =>  e.Result)
            .Include(e => e.Exercise)
            .ThenInclude(e =>e.MathType)
            .Include(e => e.Exercise)
            .ThenInclude(e =>e.Level)
            .FirstOrDefaultAsync(r => r.SolutionId == id && r.UserId== userId.ToString());
        if (s != null)
        {
            return new SolutionDto
            {
                SolutionId = s.SolutionId,
                MathTypeName = s.Exercise.MathType,
                Level = s.Exercise.Level,
                ResultValue = s.Exercise.Result.ResultValues,
                ElapsedTime = s.ElapsedTime,
                SolvedAt = s.SolvedAt,
                CreatedAt = s.CreatedAt,
                UserId = s.UserId,
                Question = s.Exercise.Question
            };
        }

        return null;
    }

    public async Task<IEnumerable<SolutionDto>> GetAllByUserIdAsync(string userId)
    {
        return await _context.Solutions
            .Where(s => s.UserId == userId)
            .Where(s => s.SolvedAt != DateTime.MinValue)
            .Select(s => new SolutionDto
            {
                SolutionId = s.SolutionId,
                MathTypeName = s.Exercise.MathType,
                Level = s.Exercise.Level,
                ResultValue = s.Exercise.Result.ResultValues,
                ElapsedTime = s.ElapsedTime,
                SolvedAt = s.SolvedAt,
                CreatedAt = s.CreatedAt,
                UserId = s.UserId,
                Question = s.Exercise.Question
            })
            .ToListAsync();
        
    }

    public async Task<IEnumerable<SolutionDto>> GetByUserIdIntervalAsync(string userId, DateTime fromDate, DateTime toDate)
    {
        return await _context.Solutions
            .Where(s => s.UserId == userId)
            .Where(s => s.SolvedAt >= fromDate && s.SolvedAt <= toDate)
            .Select(s => new SolutionDto
            {
                SolutionId = s.SolutionId,
                MathTypeName = s.Exercise.MathType,
                Level = s.Exercise.Level,
                ResultValue = s.Exercise.Result.ResultValues,
                ElapsedTime = s.ElapsedTime,
                SolvedAt = s.SolvedAt,
                CreatedAt = s.CreatedAt,
                UserId = s.UserId,
                Question = s.Exercise.Question
            })
            .ToListAsync();
    }
}