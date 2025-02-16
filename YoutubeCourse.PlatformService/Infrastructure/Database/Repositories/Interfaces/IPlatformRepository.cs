using System.Collections.Generic;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Infrastructure.Database.Repositories.Interfaces;

public interface IPlatformRepository
{
    bool SaveChanges();
    
    IEnumerable<Platform> GetAllPlatforms();
    
    Platform GetPlatformById(int id);
    
    void CreatePlatform(Platform platform);
}