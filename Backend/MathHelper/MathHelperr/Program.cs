using System.Text;
using MathHelperr.Data;
using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Authentication;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Groq;
using MathHelperr.Service.LevelProvider;
using MathHelperr.Service.Math.Factory;
using MathHelperr.Service.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddUserSecrets<Program>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAlgebraExcercise, AlgebraExerciseFromAbstract>();
builder.Services.AddScoped<IMultiplicationExcercise, MultiplicationExerciseFromAbstract>();
builder.Services.AddScoped<IDivisionExcercise, DivisionExerciseFromAbstract>();
builder.Services.AddScoped<IRemainDivisionExcercise, RemainDivisionExerciseFromAbstract>();

//original factory, new implementation because refactoring - it runs with this!!!!!
//builder.Services.AddScoped<IMathFactory, MathFactory>();

//Get "level" data from htttp context or later from desctop application
//HttpContextAccessor - acces to http content
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IContextProvider, WebContextProvider>();

builder.Services.AddScoped<GroqRequest>();
builder.Services.AddScoped<GroqApiClient>();
builder.Services.AddScoped<GroqTextGenerator>();
builder.Services.AddScoped<IGroqResultGenerator, GroqResultGenerator>();
builder.Services.AddScoped<ICreatorRepository, CreatorRepository>();
builder.Services.AddScoped<IRepositoryUserData<SolutionDto>, SolutionDtoRepository>();


//IGenerator Branch registration test
builder.Services.AddScoped<IAlgebraExampleGenerator, Level1AlgebraExampleGenerator>();
builder.Services.AddScoped<IAlgebraExampleGenerator, Level2AlgebraExampleGenerator>();
builder.Services.AddScoped<IAlgebraExampleGenerator, Level3AlgebraExampleGenerator>();
// builder.Services.AddScoped<IAlgebraTextGenerator, Level1AlgebraTextGenerator>();
// builder.Services.AddScoped<IAlgebraTextGenerator, Level2AlgebraTextGenerator>();
// builder.Services.AddScoped<IAlgebraTextGenerator, Level3AlgebraTextGenerator>();

builder.Services.AddScoped<IDivisionExampleGenerator, Level1DivisionExampleGenerator>();
builder.Services.AddScoped<IDivisionTextGenerator, Level1DivisionTextGenerator>();

// builder.Services.AddScoped<IMultiplicationTextGenerator, Level1MultiplicationTextGenerator>();
// builder.Services.AddScoped<IMultiplicationTextGenerator, Level2MultiplicationTextGenerator>();
// builder.Services.AddScoped<IMultiplicationTextGenerator, Level3MultiplicationTextGenerator>();
builder.Services.AddScoped<IMultiplicationExampleGenerator, Level1MultiplicationExampleGenerator>();
builder.Services.AddScoped<IMultiplicationExampleGenerator, Level2MultiplicationExampleGenerator>();
builder.Services.AddScoped<IMultiplicationExampleGenerator, Level3MultiplicationExampleGenerator>();

builder.Services.AddScoped<IRemainDivisonExampleGenerator, Level1RemainDivisionExampleGenerator>();
// builder.Services.AddScoped<IRemainDivisionTextGenerator, Level1RemainDivisionTextGenerator>();
//modified textgenerator
builder.Services.AddScoped<IMathTextGenerator, AlgebraTextGeneratorGeneral>();
builder.Services.AddScoped<IMathTextGenerator, DivisionTextGeneratorGeneral>();
builder.Services.AddScoped<IMathTextGenerator, RemainDivisionTextGeneratorGenral>();
builder.Services.AddScoped<IMathTextGenerator, MultiplicationTextGeneratorGeneral>();

builder.Services.AddScoped<IAlgebraTextGenerator, AlgebraTextGeneratorGeneral>();
builder.Services.AddScoped<IDivisionTextGenerator, DivisionTextGeneratorGeneral>();
builder.Services.AddScoped<IRemainDivisionTextGenerator, RemainDivisionTextGeneratorGenral>();
builder.Services.AddScoped<IMultiplicationTextGenerator, MultiplicationTextGeneratorGeneral>();
// // IMathExampleGenerator regisztráció
// builder.Services.AddScoped<IMathExampleGenerator, Level1AlgebraExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level2AlgebraExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level3AlgebraExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level1DivisionExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level1MultiplicationExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level2MultiplicationExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level3MultiplicationExampleGenerator>();
// builder.Services.AddScoped<IMathExampleGenerator, Level1RemainDivisionExampleGenerator>();
//
// // IMathTextGenerator regisztráció
// builder.Services.AddScoped<IMathTextGenerator, Level1AlgebraTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level2AlgebraTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level3AlgebraTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level1DivisionTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level1MultiplicationTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level2MultiplicationTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level3MultiplicationTextGenerator>();
// builder.Services.AddScoped<IMathTextGenerator, Level1RemainDivisionTextGenerator>();


  //generikus factroy registration
//builder.Services.AddScoped(typeof(IMathExampleGeneratorFactory<>), typeof(MathGeneratorFactory));
//builder.Services.AddScoped<IMathGeneratorFactory, MathGeneratorFactory>();
builder.Services.AddScoped<MathGeneratorFactory>();
builder.Services.AddScoped<IMathFactory, MathFactoryMarkingWithFactory>(); 

//builder.Services.AddScoped<AlgebraExampleGeneratorFactory>();
//builder.Services.AddScoped<AlgebraTextGeneratorFactory>();
//register repository
builder.Services.AddScoped<IRepository<Solution>, SolutionRepository>();


//authentication registration
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//AddRoles() belongs also to auth.



// IMath children registration for different levels, solution for same interface
// implementations registration
//AddAlgebraGenerators();
AddMultiplicationGenerators();
AddDivisionGenerators();

AddJwtAuthentication();
AddIdentity();

//docker-compose miatt
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? builder.Configuration["ConnectionStrings__DefaultConnection"];

// Configure Identity DbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
  //  options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AuthenticationSeeder>();


builder.Services.AddControllers();

// ssl tanustvány miatt lehet majd törlöd docker miatt 
// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(443, listenOptions =>

//     {
//         listenOptions.UseHttps("/https/localhost-cert.pem", "/https/localhost-key.pem");
//     });
//     options.ListenAnyIP(80); // HTTP
// });

//CORS settings - call before MApControllers()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", build =>
    {
        build.WithOrigins("https://localhost:5173") // Frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Fontos a sütik küldéséhez
        
    });
});

var app = builder.Build();

//Migration();

// Authentication: add identity roles
AddRoles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//CORS settings
app.UseCors("AllowAll");

app.MapControllers();

app.Run();

void Migration()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
        
        dbContext.Database.Migrate();
        userContext.Database.Migrate();
    }
}


void AddRoles()
{
    using var scope = app.Services.CreateScope(); // AuthenticationSeeder is a scoped service, therefore we need a scope instance to access it
    var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
    authenticationSeeder.AddRoles(); //User and Admin roles 
    authenticationSeeder.AddAdmin(); // admin created and added
}


void AddDivisionGenerators()
{
    builder.Services.AddScoped<IDivisionExampleGenerator>(provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1DivisionExampleGenerator();
            default:
                return new Level1DivisionExampleGenerator();
        }

        throw new InvalidOperationException();
    });
    
    builder.Services.AddScoped<IDivisionTextGenerator>(provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1DivisionTextGenerator();
            default:
                return new Level1DivisionTextGenerator();
        }

        throw new InvalidOperationException();
    });
    
}

void AddMultiplicationGenerators()
{
    
    builder.Services.AddScoped<IMultiplicationTextGenerator>(provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1MultiplicationTextGenerator();
            case "2":
                return new Level2MultiplicationTextGenerator();
            case "3":
                return new Level3MultiplicationTextGenerator();
            default:
                return new Level1MultiplicationTextGenerator();
        }

        throw new InvalidOperationException();
    });
    
    builder.Services.AddScoped<IMultiplicationExampleGenerator>(implementationFactory: provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1MultiplicationExampleGenerator();
            case "2":
                return new Level2MultiplicationExampleGenerator();
            case "3":
                return new Level3MultiplicationExampleGenerator();
            default:
                return new Level1MultiplicationExampleGenerator();
        }
        throw new InvalidOperationException();
    });

    builder.Services.AddScoped<IRemainDivisonExampleGenerator>(implementationFactory: provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1RemainDivisionExampleGenerator();
           default:
                return new Level1RemainDivisionExampleGenerator();
        }
        throw new InvalidOperationException();
    });
    
    builder.Services.AddScoped<IRemainDivisionTextGenerator>(implementationFactory: provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1RemainDivisionTextGenerator();
            default:
                return new Level1RemainDivisionTextGenerator();
        }
        throw new InvalidOperationException();
    });
    
    
}

void AddAlgebraGenerators()
{
    builder.Services.AddScoped<IAlgebraTextGenerator>(provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1AlgebraTextGenerator();
            case "2":
                return new Level2AlgebraTextGenerator();
            case "3":
                return new Level3AlgebraTextGenerator();
            default:
                return new Level1AlgebraTextGenerator();
        }

        throw new ArgumentException("Invalid level");
    });

    builder.Services.AddScoped<IAlgebraExampleGenerator>(provider =>
    {
        var context = provider.GetRequiredService<IContextProvider>();
        var level = context.GetLevel();
        switch (level)
        {
            case "1":
                return new Level1AlgebraExampleGenerator();
            case "2":
                return new Level2AlgebraExampleGenerator();
            case "3":
                return new Level3AlgebraExampleGenerator();
            default:
                return new Level1AlgebraExampleGenerator();
        }

        throw new ArgumentException("Invalid level");
    });
}

void AddJwtAuthentication()
{



    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var issuerSigningKey = builder.Configuration["Jwt:IssuerSigningKey"];



    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudience = jwtSettings["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey))
            };
            options.Events = new JwtBearerEvents
            {

                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Authentication failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token validated for user: " + context.Principal.Identity.Name);
                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    Console.WriteLine("JWT Token received: " + context.Token);
                    context.Token = context.Request.Cookies[
                        jwtSettings["CookieName"] ??
                        throw new InvalidOperationException(
                            "no CookieName key in appsettings")]; // A token cookie-ból való kiolvasása
                    return Task.CompletedTask;
                }
            };

        });
//outgoing settings
    // .AddCookie(options =>
    // {
    //     options.Cookie.HttpOnly = true;
    //     options.Cookie.SecurePolicy =
    //         CookieSecurePolicy.Always; // Set to Always in production, Csak HTTPS-en keresztül
    //     options.Cookie.SameSite = SameSiteMode.None; //Cross origin cookies
    //     options.Cookie.Name = "jwt";
    //     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    //     
    // });

}

void AddIdentity()
{
//https://journey.study/v2/learn/materials/asp-register-2q2023
    builder.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>() //Enable Identity roles 
        .AddEntityFrameworkStores<UserContext>()
        .AddDefaultTokenProviders();
    
}
public partial class Program { }