using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/author")]
    [Authorize]
    public class AuthorController:ControllerBase
    {
        private IAuthorService _service;

        public AuthorController (IAuthorService service)
        {
            _service = service;
        }

        [HttpGet("getAuthor")]
        public async Task<ActionResult<Author>> GetAuthor ([FromQuery] string name)
        {
            return Ok(await _service.GetAuthorByName(name));
        }

        [HttpGet("getAllAuthors")]
        public async Task<ActionResult<List<Author>>> GetAuthorList([FromQuery] int pageNumber = 1, int pageSize = 10) {
            return Ok(await _service.GetAllAuthors(pageNumber, pageSize));
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Author>> AddAuthor([FromBody] string authorName)
        {
            Author currentAuthor = await _service.CreateAuthor(authorName);
            return Ok(currentAuthor);
        }


        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Author>> UpdateAuthor([FromQuery] string oldAuthor, [FromQuery] string newAuthor)
        {
            return Ok(await _service.UpdateAuthorName(oldAuthor, newAuthor));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> DeleteAuthor([FromQuery] string authorName)
        {
            int deleted = await _service.DeleteAuthorByName(authorName);
            if(deleted==-1)
            {
                return NotFound(authorName+" is not one of the authors");
            }else if(deleted==0)
            {
                return BadRequest("Unexpected error encountered");
            }
            return Ok(deleted + " author deleted");
        }

    }
}
