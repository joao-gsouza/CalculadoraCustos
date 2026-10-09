using CalculadoraCustos.Application.Services;
using CalculadoraCustos.Domain.Interfaces;
using CalculadoraCustos.Infrastructure.Data;
using CalculadoraCustos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPratoRepository, PratoRepository>();
builder.Services.AddScoped<IProdutoBaseRepository, ProdutoBaseRepository>();

builder.Services.AddScoped<ProdutoBaseService>();
builder.Services.AddScoped<PratoService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
