using Microsoft.AspNetCore.Mvc;
using Task5.Generators;

namespace Task5.Controllers;

[ApiController]
[Route("api/cover")]
public class CoverController : ControllerBase
{
    private readonly CoverGenerator _generator;

    public CoverController(CoverGenerator generator)
    {
        _generator = generator;
    }

    [HttpGet]
    public IActionResult Get(long seed, int page, int index, string locale, string album, string artist)
    {
        var bytes = _generator.Generate(seed, page, index, locale, album, artist);
        return File(bytes, "image/png");
    }
}