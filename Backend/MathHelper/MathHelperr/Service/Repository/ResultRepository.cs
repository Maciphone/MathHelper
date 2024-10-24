using MathHelperr.Data;
using MathHelperr.Model.Db;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Service;

public class ResultRepository : IRepository<Result>
{
    private readonly ApplicationDbContext _context;

    public ResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET BY ID
    public async Task<Result?> GetByIdAsync(int id)
    {
        return await _context.Results
            .FirstOrDefaultAsync(r => r.ResultId == id);
    }

    // GET ALL
    public async Task<IEnumerable<Result?>> GetAllAsync()
    {
        return await _context.Results
            .ToListAsync();
    }

    // ADD
    public async Task AddAsync(Result? result)
    {
        _context.Results.Add(result);
        await _context.SaveChangesAsync();
    }

    // UPDATE
    public async Task UpdateAsync(Result? result)
    {
        _context.Results.Update(result);
        await _context.SaveChangesAsync();
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var result = await _context.Results.FindAsync(id);
        if (result != null)
        {
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
