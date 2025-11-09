using TestApplication.Models;

namespace TestApplication.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookByTitle(string title);
        Task<Book> CreateBook(Book book);
        Task<Book?> UpdateBook(Book book, Book newBook);
        Task<int> DeleteBookByTitle(string title);

        Task<List<Book>> GetAllBooksByAuthor(Author author, int pageNumber, int pageSize);
        Task<List<Book>> GetAllBooksByGenre(Genre genre, int pageNumber, int pageSize);

        Task<Book?> IssueBook(Book book);
        Task<Book?> ReturnBook(Book book);
    }
}
