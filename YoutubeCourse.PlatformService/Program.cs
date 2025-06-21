using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YoutubeCourse.PlatformService.Domain.Interfaces;
using YoutubeCourse.PlatformService.Infrastructure.Database;
using YoutubeCourse.PlatformService.Infrastructure.Database.Repositories;
using YoutubeCourse.PlatformService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("[DEBUG] Using InMemoryDatabase");
    builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseInMemoryDatabase("InMemoryDatabase"));
}
else
{
    Console.WriteLine("[DEBUG] Using SQL Server");
    builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<ICommandDataClientService, CommandDataClientService>();

builder.Services.AddControllers();

Console.WriteLine($"[DEBUG] CommandService endpoint: {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

DbHelper.SeedDb(app, app.Environment.IsProduction());

app.Run();
