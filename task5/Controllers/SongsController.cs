using Microsoft.AspNetCore.Mvc;
using task5.Generators;

namespace task5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly SongGenerator _generator;

        public SongsController(SongGenerator generator)
        {
            _generator = generator;
        }

        [HttpGet]
        public IActionResult Get(
            long seed = 1,
            int page = 1,
            string locale = "en_US",
            double likes = 3.5)
        {
            var songs = _generator.Generate(seed, page, locale, likes);

            return Ok(songs);
        }
    }
}
