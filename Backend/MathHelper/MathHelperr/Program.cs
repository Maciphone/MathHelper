using MathHelperr.Service;
using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
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

builder.Services.AddTransient<IAlgebraExampleGenerator, AlgebraExampleGenerator>();
builder.Services.AddTransient<IAlgebraTextGenerator, AlgebraTextGenerator>();
builder.Services.AddTransient<IAlgebraExcercise, AlgebraExerciseFromAbstract>();

builder.Services.AddTransient<IMultiplicationExampleGenerator, MultiplicationExampleGenerator>();
builder.Services.AddTransient<IMultiplicationTextGenerator, MultiplicationTextGenerator>();
builder.Services.AddTransient<IMultiplicationExcercise, MultiplicationExerciseFromAbstract>();

builder.Services.AddScoped<IMathFactory, MathFactory>();

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

