using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/books")]
    [Authorize]
    public class BookController: ControllerBase
    {
        private IBookService _service;
        private IGenreService _genre;
        private IAuthorService _author;

        public BookController(IBookService service, IGenreService genre, IAuthorService author)
        {
            _service = service;
            _genre = genre;
            _author = author;
        }
        [HttpGet("{title}")]
        public ActionResult<Book> GetByTitle([FromRoute] string title)
        {
            return Ok(_service.GetBookByTitle(title).Result);
        }

        [HttpGet("GetAllWithAuthor")]
        public ActionResult<List<Book>> getAllBooksWithAuthor([FromQuery] string author, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=10)
        {
            Author? currentAuthor = _author.GetAuthorByName(author).Result;
            if (currentAuthor == null)
            {
                return NotFound("Author Not Found");
            }
            else if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Invalid pagination parameters. Pagenumber and Pagesize should be greater than 0");
            }
            return Ok(_service.GetAllBooksByAuthor(currentAuthor, pageNumber, pageSize).Result);
        }

        [HttpGet("GetAllWithGenre")]
        public ActionResult<List<Book>> GetAllBooksWithGenre([FromQuery] string genre,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) {
            Genre? currentGenre = _genre.GetGenreByName(genre).Result;
            if (currentGenre == null) {
                return NotFound("Genre Not Found");
            }else if(pageNumber<1 || pageSize<1)
            {
                return BadRequest("Invalid pagination parameters. Pagenumber and Pagesize should be greater than 0");
            }
            return Ok(_service.GetAllBooksByGenre(currentGenre, pageNumber, pageSize).Result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book> CreateBook([FromBody] Book book) {
            return Ok(_service.CreateBook(book));
        }

        [HttpPatch("UpdateBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book?> UpdateBook([FromHeader] String oldBookTitle, [FromBody] Book newBook) {
            Book oldBook = _service.GetBookByTitle(oldBookTitle).Result;
            if (oldBook == null) {
                return NotFound(oldBook + " not found.");
            }
            return Ok(_service.UpdateBook(oldBook, newBook));
        }

        [HttpPatch("IssueBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book?> IssueBook([FromQuery] string bookTitle)
        {
            Book currentBook = _service.GetBookByTitle(bookTitle).Result;
            if (currentBook == null)
            {
                return NotFound(bookTitle + " cannot be found in library");
            }
            if (currentBook.Available == 0)
            {
                return Accepted(bookTitle + " is not currently in library.");
            }
            return Ok(_service.IssueBook(currentBook));
        }

        [HttpPatch("ReturnBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book?> ReturnBook([FromHeader] string bookTitle) {
            Book currentBook = _service.GetBookByTitle(bookTitle).Result;
            if (currentBook == null) {
                return NotFound(bookTitle+" not found in library.");
            }
            if (currentBook.Available == 1)
            {
                return Accepted(bookTitle + " is not issued.");
            }
            return Ok(_service.ReturnBook(currentBook));
        }

        [HttpDelete("{bookTitle}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBook([FromRoute] string bookTitle)
        {
            int deleted = _service.DeleteBookByTitle(bookTitle).Result;
            if (deleted == 0)
            {
                return NotFound(bookTitle + " not found in library");
            }
            else if (deleted == -1) {
                return Problem("Unknown issue with the application");
            }
            return Ok("Books Deleted: " + deleted);
        }
    }
}
