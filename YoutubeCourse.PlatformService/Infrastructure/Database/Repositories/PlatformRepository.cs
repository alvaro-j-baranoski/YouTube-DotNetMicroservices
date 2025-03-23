using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeCourse.PlatformService.Domain.Interfaces;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Infrastructure.Database.Repositories;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _databaseContext;

    public PlatformRepository(AppDbContext context)
    {
        _databaseContext = context;
    }
    
    public bool SaveChanges()
    {
        return _databaseContext.SaveChanges() >= 0;
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _databaseContext.Platforms.ToList();
    }

    public Platform GetPlatformById(int id)
    {
        return _databaseContext.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public void CreatePlatform(Platform platform)
    {
        ArgumentNullException.ThrowIfNull(platform);
        _databaseContext.Platforms.Add(platform);
    }
}