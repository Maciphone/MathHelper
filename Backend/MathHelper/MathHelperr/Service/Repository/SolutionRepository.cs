using MathHelperr.Data;
using MathHelperr.Model.Db;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Service.Repository;

public class SolutionRepository : IRepository<Solution>
{
    private readonly ApplicationDbContext _context;

    public SolutionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    

    public async Task<Solution?> GetByIdAsync(int id)
    {
        return await _context.Solutions
            .FirstOrDefaultAsync(r => r.SolutionId == id);
    }

    public async Task<IEnumerable<Solution?>> GetAllAsync()
    {
        return await _context.Solutions.ToListAsync();
    }

    public async Task AddAsync(Solution entity)
    {
        await _context.Solutions.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Solution entity)
    {
        _context.Solutions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Solutions.FindAsync(id);
        if (entity != null)
        {
            _context.Solutions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    

    
}