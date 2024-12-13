using System.Security.Cryptography.X509Certificates;
using System.Text;
using DotNetEnv;
using MathHelperr.Data;
using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Authentication;
using MathHelperr.Service.Encription;
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
Env.Load();

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


builder.Services.AddScoped<IDivisionExampleGenerator, Level1DivisionExampleGenerator>();



builder.Services.AddScoped<IMultiplicationExampleGenerator, Level1MultiplicationExampleGenerator>();
builder.Services.AddScoped<IMultiplicationExampleGenerator, Level2MultiplicationExampleGenerator>();
builder.Services.AddScoped<IMultiplicationExampleGenerator, Level3MultiplicationExampleGenerator>();

builder.Services.AddScoped<IRemainDivisonExampleGenerator, Level1RemainDivisionExampleGenerator>();
// builder.Services.AddScoped<IRemainDivisionTextGenerator, Level1RemainDivisionTextGenerator>();
//modified textgenerator

builder.Services.AddScoped<IAlgebraTextGenerator, AlgebraTextGeneratorGeneral>();
builder.Services.AddScoped<IDivisionTextGenerator, DivisionTextGeneratorGeneral>();
builder.Services.AddScoped<IRemainDivisionTextGenerator, RemainDivisionTextGeneratorGenral>();
builder.Services.AddScoped<IMultiplicationTextGenerator, MultiplicationTextGeneratorGeneral>();



  //generikus factroy registration
//builder.Services.AddScoped(typeof(IMathExampleGeneratorFactory<>), typeof(MathGeneratorFactory));
//builder.Services.AddScoped<IMathGeneratorFactory, MathGeneratorFactory>();
builder.Services.AddScoped<IMathGeneratorFactory, MathGeneratorFactory>();
builder.Services.AddScoped<IMathFactory, MathFactoryMarkingWithFactory>(); 

//register repository
builder.Services.AddScoped<IRepository<Solution>, SolutionRepository>();

//register encription
builder.Services.AddScoped<IEncription, Encriptor>();


//authentication registration
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//AddRoles() belongs also to auth.



AddJwtAuthentication();
AddIdentity();

//docker-compose miatt
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Identity DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Hány próbálkozás legyen
            maxRetryDelay: TimeSpan.FromSeconds(10), // Mekkora késleltetéssel
            errorNumbersToAdd: null); // További hibaazonosítók, ha szükséges
    }));
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString,
    sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Hány próbálkozás legyen
            maxRetryDelay: TimeSpan.FromSeconds(10), // Mekkora késleltetéssel
            errorNumbersToAdd: null); // További hibaazonosítók, ha szükséges
    }));
//eddig docker 

//docker testelés előtt
//builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AuthenticationSeeder>();


builder.Services.AddControllers();

// ssl tanustvány miatt lehet majd törlöd docker miatt 
builder.WebHost.ConfigureKestrel(options =>
{
    Console.WriteLine($"CERT_PATH: {Environment.GetEnvironmentVariable("CERT_PATH")}");
    Console.WriteLine($"CERT_PASSWORD: {Environment.GetEnvironmentVariable("CERT_PASSWORD")}");

    var certPath = Environment.GetEnvironmentVariable("CERT_PATH");
    var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");
    var certificate = new X509Certificate2(certPath, certPassword, X509KeyStorageFlags.MachineKeySet);
    options.ListenAnyIP(443, listenOptions =>
    {
        
        listenOptions.UseHttps(certificate);
    });
    options.ListenAnyIP(80); // HTTP
});

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Fejlesztői hibakezelő oldal engedélyezése
}

Migration();

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

        //Test nem fut le, ha nem relációs adatbázisból fut a migráció
        if (dbContext.Database.IsRelational()) // Csak relációs adatbázis esetén fut
        {
            dbContext.Database.Migrate();
            userContext.Database.Migrate();
        }
    }
}


void AddRoles()
{
    using var scope = app.Services.CreateScope(); // AuthenticationSeeder is a scoped service, therefore we need a scope instance to access it
    var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
    authenticationSeeder.AddRoles(); //User and Admin roles 
    authenticationSeeder.AddAdmin(); // admin created and added
}



void AddJwtAuthentication()
{



    var jwtSettings = builder.Configuration.GetSection("Jwt");
    //var issuerSigningKey = builder.Configuration["Jwt:IssuerSigningKey"];
    var issuerSigningKey = Environment.GetEnvironmentVariable("JWT_IssuerSigningKey");



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