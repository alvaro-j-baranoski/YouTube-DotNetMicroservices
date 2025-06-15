using System;
using Microsoft.AspNetCore.Mvc;

namespace YoutubeCourse.CommandService.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {
        
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine($"[DEBUG] {nameof(TestInboundConnection)}");
        return Ok("Inbound test from Platforms Controller");
    }
}