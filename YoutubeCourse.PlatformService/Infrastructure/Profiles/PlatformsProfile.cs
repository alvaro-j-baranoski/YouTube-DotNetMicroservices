using AutoMapper;
using YoutubeCourse.PlatformService.Domain.Dtos;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Infrastructure.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}