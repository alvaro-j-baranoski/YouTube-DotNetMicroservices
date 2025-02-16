using Microsoft.EntityFrameworkCore;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}