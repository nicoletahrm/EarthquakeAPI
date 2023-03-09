using Earthquake.API.Processor;
using Earthquake.API.Services;
using Earthquake.API.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IEarthquakeProcessor, EarthquakeProcessor>();
builder.Services.AddSingleton<IEarthquakeRepository, EarthquakeRepository>();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "CorsPolicy",
//                              policy =>
//                              {
//                                  policy.WithOrigins("http://localhost:4200")
//                                  .AllowAnyHeader()
//                                  .AllowAnyMethod()
//                                  .AllowCredentials();
//                              });
//});

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
