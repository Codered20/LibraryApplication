using Microsoft.EntityFrameworkCore;
using TestApplication.Data;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class GenreService : IGenreService
    {
        AppDbContext _context;

        public GenreService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Genre?> CreateGenre(string name)
        {
            bool exists = _context.Genres.AnyAsync(g => g.GenreName == name).Result;
            if (!exists)
            {
                try
                {
                    using var transaction = await _context.Database.BeginTransactionAsync();
                    var genre = new Genre() { GenreName = name };
                    _context.Genres.Add(genre);
                    _context.SaveChanges();
                    await transaction.CommitAsync();
                    return genre;
                }
                catch (Exception ex)
                {
                    await _context.Database.RollbackTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                return await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == name);
            }
        }

        public Task<bool> DeleteGenreByName(string name)
        {
            bool exists = _context.Genres.AnyAsync(g => g.GenreName == name).Result;
            if(exists)
            {
                try
                {
                    using var transaction = _context.Database.BeginTransaction();
                    var genre = _context.Genres.FirstOrDefaultAsync(g => g.GenreName == name).Result;
                    
                    _context.Genres.Remove(genre!);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                    Console.WriteLine(ex.Message);
                    return Task.FromResult(false);
                }
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<List<Genre>> GetAllGenres(int pageNumber, int pageSize)
        {
            return _context.Genres.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public Task<Genre?> GetGenreByName(string name)
        {
            return _context.Genres.FirstOrDefaultAsync(g => g.GenreName == name);
        }

        public Task<Genre?> UpdateGenreName(string oldGenre, string newName)
        {
            bool newExists = _context.Genres.AnyAsync(g => g.GenreName == newName).Result;
            if (newExists)
            {
                return Task.FromResult<Genre?>(null);
            }
            bool oldExists = _context.Genres.AnyAsync(g => g.GenreName == oldGenre).Result;
            if (oldExists)
            {
                try
                {
                    using var transaction = _context.Database.BeginTransaction();
                    var genre = _context.Genres.FirstOrDefaultAsync(g => g.GenreName == oldGenre).Result;
                    genre!.GenreName = newName;
                    _context.Genres.Update(genre);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Task.FromResult<Genre?>(genre);
                }
                catch (Exception ex)
                {
                    _context.Database.RollbackTransaction();
                    Console.WriteLine(ex.Message);
                    return Task.FromResult<Genre?>(null);
                }
            }
            else
            {
                return Task.FromResult<Genre?>(null);
            }
        }
    }
}
