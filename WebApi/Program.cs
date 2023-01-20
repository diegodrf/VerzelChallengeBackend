using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WebApi.ApplicationConstants;
using WebApi.Repositories;
using WebApi.Repositories.Interfaces;
using WebApi.Services;
using WebApi.Services.DependencyInjection;
using WebApi.Services.Interfaces;
using static Dapper.SqlMapper;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

// Add services to the container.
builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAuthenticationService(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
