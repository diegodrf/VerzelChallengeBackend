using Microsoft.Extensions.FileProviders;
using System.Reflection;
using WebApi.Services.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//builder
//    .Configuration
//    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

// Add services to the container.
builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAuthenticationService(builder.Configuration);

builder.Services.AddCors();

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

app.UseCors(options =>
{
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), @"Assets", "Images")),
    RequestPath = new PathString("/images")
});

app.MapControllers();

app.Run();
