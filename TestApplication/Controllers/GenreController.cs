using Microsoft.AspNetCore.Mvc;
using TestApplication.Interfaces;
using TestApplication.Models;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService service;

        public GenreController(IGenreService service)
        {
            this.service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Genre>>> GetAllGenres()
        {
            var allGenres = await service.GetAllGenres();
            return Ok(allGenres);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Genre>> Get([FromRoute] string name)
        {
            var genre = await service.GetGenreByName(name);
            return Ok(genre);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Genre>> Create([FromBody] Genre genre)
        {
            var created = await service.CreateGenre(genre.GenreName);
            return Ok(created);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Genre>> Update([FromQuery] string oldName, [FromHeader] string newName)
        {
            var updated = await service.UpdateGenreName(oldName, newName);
            return Ok(updated);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] string name)
        {
            var deleted = await service.DeleteGenreByName(name);
            return Ok(deleted);
        }
    }
}
