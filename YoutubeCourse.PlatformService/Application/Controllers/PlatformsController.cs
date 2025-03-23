using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YoutubeCourse.PlatformService.Domain.Dtos;
using YoutubeCourse.PlatformService.Domain.Interfaces;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;

namespace YoutubeCourse.PlatformService.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController(
    IPlatformRepository platformRepository, 
    IMapper mapper, 
    ICommandDataClientService commandDataClientService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("[DEBUG] Getting all platforms");
        
        var platforms = platformRepository.GetAllPlatforms();
        var result = mapper.Map<IEnumerable<PlatformReadDto>>(platforms); 
        
        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        Console.WriteLine("[DEBUG] Getting platform by id");
        
        var platform = platformRepository.GetPlatformById(id);

        if (platform == null)
        {
            return NotFound();
        }
        
        var result = mapper.Map<PlatformReadDto>(platform);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        Console.WriteLine("[DEBUG] Creating platform");
        
        var platformModel = mapper.Map<Platform>(platformCreateDto);
        platformRepository.CreatePlatform(platformModel);
        platformRepository.SaveChanges();
        
        var result = mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await commandDataClientService.SendPlatformToCommand(result);
        }
        catch (Exception e)
        {
            Console.WriteLine($"[DEBUG] Could not send synchronously: {e}");
            throw;
        }
        
        return CreatedAtRoute(nameof(GetPlatformById), new { platformModel.Id }, result);
    }
}