using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
using MathHelperr.Service.Groq;
using MathHelperr.Service.LevelProvider;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Scrutor használata
// builder.Services.Scan(scan => scan
//     .FromAssemblyOf<AlgebraExampleGenerator>()
//     .AddClasses(classes => classes.AssignableTo<IMathExampleGenerator>())
//     .AsImplementedInterfaces()
//     .WithTransientLifetime());
//
// builder.Services.Scan(scan => scan
//     .FromAssemblyOf<AlgebraTextGenerator>()
//     .AddClasses(classes => classes.AssignableTo<IMathTextGenerator>())
//     .AsImplementedInterfaces()
//     .WithTransientLifetime());
//
// builder.Services.Scan(scan => scan
//     .FromAssemblyOf<AlgebraExcercise>()
//     .AddClasses(classes => classes.AssignableTo<IMathExcercise>())
//     .AsImplementedInterfaces()
//     .WithTransientLifetime());
//
// builder.Services.AddTransient<MultiplicationExampleGenerator>();
// builder.Services.AddTransient<MultiplicationTextGenerator>();
// builder.Services.AddTransient<MultiplicationExcercise>();
//
// builder.Services.AddScoped<IMathFactory, MathFactory>();

//builder.Services.AddTransient<IAlgebraExampleGenerator, AlgebraExampleGenerator>();
//builder.Services.AddTransient<IAlgebraTextGenerator, AlgebraTextGenerator>();
//builder.Services.AddTransient<IMultiplicationExampleGenerator, MultiplicationExampleGenerator>();
//builder.Services.AddTransient<IMultiplicationTextGenerator, MultiplicationTextGenerator>();

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


// IMath children registration for different levels, solution for same interface
// implementations registration
AddAlgebraGenerators();
AddMultiplicationGenerators();
AddDivisionGenerators();






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