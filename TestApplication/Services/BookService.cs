using TestApplication.Data;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public Task<Book> CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return Task.FromResult(book);
        }

        public Task<bool> DeleteBookByTitle(string title)
        {
            _context.Books.RemoveRange(_context.Books.Where(b => b.Title == title));
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<List<Book>> GetAllBooksByAuthor(Author author)
        {
            return Task.FromResult(_context.Books.Where(b => b.AuthorId == author.Id).ToList());
        }

        public Task<List<Book>> GetAllBooksByGenre(Genre genre)
        {
            return Task.FromResult(_context.Books.Where(b => b.GenreId == genre.Id).ToList());
        }

        public Task<Book> GetBookByTitle(string title)
        {
            return Task.FromResult(_context.Books.First(b => b.Title == title));
        }

        public Task<Book> IssueBook(String Title)
        {
            Book? dbBook = _context.Books.FirstOrDefault(b => b.Title == Title);
            if (dbBook == null || dbBook.Available <= 0)
            {
                throw new InvalidOperationException("Book is not available for issue.");
            }
            else
            {
                dbBook.Available -= 1;
            }
            _context.SaveChanges();
            return Task.FromResult(dbBook);
        }

        public Task<Book> ReturnBook(String title)
        {
            Book? dbBook = _context.Books.FirstOrDefault(b => b.Title == title);
            dbBook!.Available += 1;
            _context.SaveChanges();
            return Task.FromResult(dbBook);

        }

        public async Task<Book?> UpdateBook(Book oldBook, Book newBook)
        {
            try
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                Book book = _context.Books.FirstOrDefault(b => b.Title == oldBook.Title)!;
                if (book == null)
                {
                    return null;
                }
                else
                {
                    _context.Entry(book).CurrentValues.SetValues(newBook);
                    _context.SaveChanges();
                }
                await transaction.CommitAsync();
                return newBook;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine($"Error updating book: {ex.Message}");
                return null;
            }
        }
    }
}
