using MathHelperr.Model.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MathHelperr.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Exercise?> Exercises { get; set; }
    public DbSet<Result?> Results { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Solution>()
            .HasOne(s => s.Exercise)
            .WithMany(e => e.Solutions)
            .HasForeignKey(s => s.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Solution>()
            .HasOne(s => s.Result)
            .WithMany(r => r.Solutions)
            .HasForeignKey(s => s.ResultId)
            .OnDelete(DeleteBehavior.NoAction);  
    }
    
  
    
}