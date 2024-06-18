using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PeraInvest;
using PeraInvest.Adapters.Persistence.Context;
using System.Runtime;

// program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<UsuarioContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();