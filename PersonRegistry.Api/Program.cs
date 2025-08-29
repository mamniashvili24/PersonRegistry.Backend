using Microsoft.EntityFrameworkCore;
using PersonRegistry.Api.Middlewares.Exceptions;
using PersonRegistry.Application;
using PersonRegistry.Persistence;
using PersonRegistry.Persistence.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddProblemDetailsMiddleware();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandlingMiddleware();

app.Run();