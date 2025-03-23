using System.Threading.Tasks;
using YoutubeCourse.PlatformService.Domain.Dtos;

namespace YoutubeCourse.PlatformService.Domain.Interfaces;

public interface ICommandDataClientService
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}