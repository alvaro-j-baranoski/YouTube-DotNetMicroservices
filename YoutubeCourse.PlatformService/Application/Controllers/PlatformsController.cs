using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YoutubeCourse.PlatformService.Domain.Dtos;
using YoutubeCourse.PlatformService.Infrastructure.Database.Models;
using YoutubeCourse.PlatformService.Infrastructure.Database.Repositories.Interfaces;

namespace YoutubeCourse.PlatformService.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _platformRepository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepository platformRepository, IMapper mapper)
    {
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("[DEBUG] Getting all platforms");
        
        var platforms = _platformRepository.GetAllPlatforms();
        var result = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms); 
        
        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        Console.WriteLine("[DEBUG] Getting platform by id");
        
        var platform = _platformRepository.GetPlatformById(id);

        if (platform == null)
        {
            return NotFound();
        }
        
        var result = _mapper.Map<PlatformReadDto>(platform);
        
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        Console.WriteLine("[DEBUG] Creating platform");
        
        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _platformRepository.CreatePlatform(platformModel);
        _platformRepository.SaveChanges();
        
        var result = _mapper.Map<PlatformReadDto>(platformModel);
        
        return CreatedAtRoute(nameof(GetPlatformById), new { platformModel.Id }, result);
    }
}