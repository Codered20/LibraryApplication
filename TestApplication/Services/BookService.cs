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

        public async Task<int> DeleteBookByTitle(string title)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                _context.Books.RemoveRange(_context.Books.Where(b => b.Title == title));
                int deleted = _context.SaveChanges();
                await transaction.CommitAsync();
                return deleted;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public Task<List<Book>> GetAllBooksByAuthor(Author author, int pageNumber, int pageSize)
        {
            return Task.FromResult(_context.Books.Where(b => b.AuthorId == author.Id).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList());
        }

        public Task<List<Book>> GetAllBooksByGenre(Genre genre, int pageNumber, int pageSize)
        {
            return Task.FromResult(_context.Books.Where(b => b.GenreId == genre.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public Task<Book> GetBookByTitle(string title)
        {
            return Task.FromResult(_context.Books.First(b => b.Title == title));
        }

        public async Task<Book?> IssueBook(Book book)
        {
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                book.Available -= 1;
                _context.SaveChanges();
                await transaction.CommitAsync();
                return book;
            }
            catch (Exception ex) {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<Book?> ReturnBook(Book dbBook)
        {
            try
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    dbBook!.Available += 1;
                    _context.SaveChanges();
                    await transaction.CommitAsync();
                    return dbBook;
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return null;
            }
            
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
