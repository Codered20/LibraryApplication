using TestApplication.Models;

namespace TestApplication.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookByTitle(string title);
        Task<Book> CreateBook(Book book);
        Task<Book?> UpdateBook(Book book, Book newBook);
        Task<bool> DeleteBookByTitle(string title);

        Task<List<Book>> GetAllBooksByAuthor(Author author);
        Task<List<Book>> GetAllBooksByGenre(Genre genre);

        Task<Book> IssueBook(String Title);
        Task<Book> ReturnBook(String Title);
    }
}
