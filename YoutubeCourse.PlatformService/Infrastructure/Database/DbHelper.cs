using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Infrastructure.Database;

public static class DbHelper
{
    public static void SeedDb(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        ExecuteSeed(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void ExecuteSeed(AppDbContext context)
    {
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