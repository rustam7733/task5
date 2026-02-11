using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using Task5.Generators;

namespace Task5.Controllers;

[ApiController]
[Route("api/cover")]
public class CoverController(CoverGenerator coverGenerator) : ControllerBase
{
    [HttpGet]
    public IActionResult Get(long seed, int index, string album, string artist)
    {
        var bytes = coverGenerator.Generate(seed, index, album, artist);
        return File(bytes, "image/png");
    }
}