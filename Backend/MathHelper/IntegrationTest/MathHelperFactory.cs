using System.Net;
using dotenv.net;
using MathHelperr.Data;
using MathHelperr.Model.Db;
using MathHelperr.Service.Authentication;
using MathHelperr.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MyIntegrationTest;

public class MathHelperFactory : WebApplicationFactory<Program>
{
    
    private readonly string _dbName = Guid.NewGuid().ToString();
    private IConfiguration _configuration; 
 
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { ".MathHelperr/.env" })); 
            config.AddEnvironmentVariables();
            // Alkalmazás konfiguráció betöltése
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = config.Build();
        });
        //https settings for  cookies
        // builder.ConfigureKestrel(options =>
        // {
        //     options.ListenLocalhost(8443, listenOptions =>
        //     {
        //         listenOptions.UseHttps(); // HTTPS engedélyezése
        //     });
        // });
        
        builder.ConfigureServices(services =>
        {
            var mathelperDbContextDescriptor = services.SingleOrDefault(descriptor => descriptor.ServiceType ==
                typeof(DbContextOptions<ApplicationDbContext>));
            var usersDbContextDescriptor = services.SingleOrDefault(d
                => d.ServiceType == typeof(DbContextOptions<UserContext>));

            services.Remove(mathelperDbContextDescriptor);
            services.Remove(usersDbContextDescriptor);

            services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase(_dbName); });
            services.AddDbContext<UserContext>(options => { options.UseInMemoryDatabase(_dbName); });

            using var scope = services.BuildServiceProvider().CreateScope();

            var mathContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            mathContext.Database.EnsureDeleted();
            mathContext.Database.EnsureCreated();

            userContext.Database.EnsureDeleted();
            userContext.Database.EnsureCreated();
            
            var seeder = new AuthenticationSeeder(roleManager, userManager, _configuration);
            seeder.AddRoles();
            seeder.AddAdmin();

            var userId = SeeduserConetext(userContext, userManager, roleManager)
                .GetAwaiter().GetResult();
            
            

            Exercise exercise = new Exercise
            {
                Question = "1 + ? = 2",
                Level = "1",
                MathType = MathTypeName.Algebra.ToString(),
                ResultId = 1
            };
            mathContext.Exercises.Add(exercise);
            mathContext.SaveChanges();

            Result result = new Result
            {
                ResultValues = [1],
                ResultHash = "1"
            };
            mathContext.Results.Add(result);
            mathContext.SaveChanges();

            Solution solution = new Solution
            {
                CreatedAt = DateTime.Now,
                ElapsedTime = 1,
                ExerciseId = 1,
                SolvedAt = DateTime.Now,
                UserId = userId
            };
            mathContext.Solutions.Add(solution);
            mathContext.SaveChanges();



        });
        //IConfiguration hozzáadása
       
    }
    
    
    private async Task<string> SeeduserConetext(UserContext userContext, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // Create roles if they don't exist
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var user = new IdentityUser { UserName = "test", Email = "a@a.hu" };
        await userManager.CreateAsync(user, "password");
        await userManager.AddToRoleAsync(user, "User"); // Adding the user to a role
        
        var userId = user.Id;
        Console.WriteLine(userId);
        
        
        
        return userId;
    }
}