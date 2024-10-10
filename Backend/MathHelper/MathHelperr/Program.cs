using System.Text;
using MathHelperr.Data;
using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Authentication;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Groq;
using MathHelperr.Service.LevelProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scrutor;
using SolarWatch.Service.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddUserSecrets<Program>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAlgebraExcercise, AlgebraExerciseFromAbstract>();
builder.Services.AddTransient<IMultiplicationExcercise, MultiplicationExerciseFromAbstract>();
builder.Services.AddTransient<IDivisionExcercise, DivisionExerciseFromAbstract>();
builder.Services.AddScoped<IMathFactory, MathFactory>();

//Get "level" data from htttp context or later from desctop application
//HttpContextAccessor - acces to http content
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IContextProvider, WebContextProvider>();

builder.Services.AddScoped<GroqRequest>();
builder.Services.AddScoped<GroqApiClient>();
builder.Services.AddScoped<GroqTextGenerator>();

//authentication registration
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();


// IMath children registration for different levels, solution for same interface
// implementations registration
AddAlgebraGenerators();
AddMultiplicationGenerators();
AddDivisionGenerators();

AddJwtAuthentication();
AddIdentity();

// Configure Identity DbContext
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

void AddDivisionGenerators()
{
    builder.Services.AddTransient<IDivisionExampleGenerator>(provider =>
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
    
    builder.Services.AddTransient<IDivisionTextGenerator>(provider =>
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
    
    builder.Services.AddTransient<IMultiplicationTextGenerator>(provider =>
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

    builder.Services.AddTransient<IMultiplicationExampleGenerator>(implementationFactory: provider =>
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
}

void AddAlgebraGenerators()
{
    builder.Services.AddTransient<IAlgebraTextGenerator>(provider =>
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
        }

        throw new ArgumentException("Invalid level");
    });

    builder.Services.AddTransient<IAlgebraExampleGenerator>(provider =>
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
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["jwt"];
                    return Task.CompletedTask;
                }
            };
        })
//outgoing settings
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy =
                CookieSecurePolicy.Always; // Set to Always in production, Csak HTTPS-en keresztül
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.Name = "jwt";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

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
        .AddEntityFrameworkStores<UserContext>();
}