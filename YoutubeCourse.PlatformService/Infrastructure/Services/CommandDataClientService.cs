using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YoutubeCourse.PlatformService.Domain.Dtos;
using YoutubeCourse.PlatformService.Domain.Interfaces;

namespace YoutubeCourse.PlatformService.Infrastructure.Services;

public class CommandDataClientService(HttpClient httpClient, IConfiguration configuration) : ICommandDataClientService
{
    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json");
        
        var response = await httpClient.PostAsync(configuration["CommandService"], httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[DEBUG] Sync POST to CommandService was OK!");
        }
        else
        {
            Console.WriteLine($"[ERROR] Sync POST to CommandService was NOT OK!");
        }
    }
}