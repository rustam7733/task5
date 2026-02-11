using Microsoft.AspNetCore.Mvc;
using task5.Generators;

namespace Task5.Controllers;

[ApiController]
[Route("api/audio")]
public class AudioController : ControllerBase
{
    private readonly AudioGenerator _audio;

    public AudioController(AudioGenerator audio)
    {
        _audio = audio;
    }

    [HttpGet]
    public IActionResult Get(long seed, int index)
    {
        var bytes = _audio.Generate(seed, index);
        return File(bytes, "audio/wav");
    }
}