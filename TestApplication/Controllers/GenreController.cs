using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Interfaces;
using TestApplication.Models;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/genre")]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService service;

        public GenreController(IGenreService service)
        {
            this.service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Genre>>> GetAllGenres([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Invalid pagination parameters. Pagenumber and Pagesize should be greater than 0");
            }
            var allGenres = await service.GetAllGenres(pageNumber, pageSize);
            return Ok(allGenres);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Genre>> Get([FromRoute] string name)
        {
            var genre = await service.GetGenreByName(name);
            return Ok(genre);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Genre>> Create([FromBody] Genre genre)
        {
            var created = await service.CreateGenre(genre.GenreName);
            return Ok(created);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Genre>> Update([FromQuery] string oldName, [FromHeader] string newName)
        {
            var updated = await service.UpdateGenreName(oldName, newName);
            return Ok(updated);
        }

        [HttpDelete("{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete([FromRoute] string name)
        {
            var deleted = await service.DeleteGenreByName(name);
            return Ok(deleted);
        }
    }
}
