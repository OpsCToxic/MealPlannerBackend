using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MealPlanBackend.Models;
using MealPlanBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MealPlannerDatabaseSettings>(builder.Configuration.GetSection(nameof(MealPlannerDatabaseSettings)));
builder.Services.AddSingleton<IMealPlannerDatabaseSettings>(sp => sp.GetRequiredService<IOptions
    <MealPlannerDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient
(builder.Configuration.GetValue<string>("MealPlannerDatabaseSettings:ConnectionURI")));
builder.Services.AddScoped<IMealService, MealService>();
// Inside ConfigureServices method in Startup.cs
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
