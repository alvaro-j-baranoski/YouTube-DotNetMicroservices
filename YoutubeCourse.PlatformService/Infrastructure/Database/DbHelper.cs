using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Infrastructure.Database;

public static class DbHelper
{
    public static void SeedDb(IApplicationBuilder applicationBuilder, bool isProduction)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        ExecuteSeed(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
    }

    private static void ExecuteSeed(AppDbContext context, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("[DEBUG] Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Could not run migrations: {ex.Message}");
            }
        }
        
        if (!context.Platforms.Any())
        {
            Console.WriteLine("[DEBUG] Seeding database...");
            
            context.Platforms.AddRange(
                new Platform() { Name = "Android", Publisher = "Android Platform", Cost = "Free" },
                new Platform() { Name = "Kubernetes", Publisher = "CNCF", Cost = "Free" },
                new Platform() { Name = "SQL Server", Publisher = "Microsoft", Cost = "Free" }
            );
            
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("[DEBUG] Database already seeded");
        }
    }
}