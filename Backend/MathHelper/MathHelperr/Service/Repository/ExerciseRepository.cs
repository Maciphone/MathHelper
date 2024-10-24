using MathHelperr.Data;
using MathHelperr.Model.Db;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Service;

public class ExerciseRepository : IRepository<Exercise>
{
    private readonly ApplicationDbContext _context;

    public ExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Exercise?> GetByIdAsync(int id)
    {
        return await _context.Exercises
            .Include(e => e.Result)
            .Include(m=>m.MathType)
            .Include(l=> l.Level)
            .FirstOrDefaultAsync(e => e.ExerciseId == id);
    }

    public async Task<IEnumerable<Exercise?>> GetAllAsync()
    {
        return await _context.Exercises
            .Include(e => e.Result)
            .Include(m=>m.MathType)
            .Include(l=> l.Level)
            .ToListAsync();
    }

    public async Task AddAsync(Exercise? entity)
    {
        _context.Exercises.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise? entity)
    {
        _context.Exercises.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Exercises.FindAsync(id);
        if (entity != null)
        {
            _context.Exercises.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}