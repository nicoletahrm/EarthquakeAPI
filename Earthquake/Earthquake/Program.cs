using Earthquake.API.Models.Requests;
using Earthquake.API.Processor;
using Earthquake.API.Services;
using Earthquake.API.Settings;
using Earthquake.API.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    //.AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEarthquakeProcessor, EarthquakeProcessor>();
builder.Services.AddSingleton<IEarthquakeRepository, EarthquakeRepository>();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient();


builder.Services.AddValidatorsFromAssemblyContaining<EartquakeRequestValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
