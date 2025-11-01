using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestApplication.Data;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Author> CreateAuthor(string name)
        {
            try
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
                    if (existingAuthor != null)
                    {
                        return existingAuthor;
                    }
                    var author = new Author() { Name = name };
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return author;
                }
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine($"Error creating author: {ex.Message}");
            }
            return null!;
        }

        public async Task<bool> DeleteAuthorByName(string name)
        {
            try
            {
                // Start and hold the transaction
                await using var transaction = await _context.Database.BeginTransactionAsync();

                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
                if (author == null)
                    return false;

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Roll back if any exception occurs
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine($"Error deleting author: {ex.Message}");
            }
            return false;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<Author?> UpdateAuthorName(string oldName, string newName)
        {
            try
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    var nameExists = await _context.Authors.FirstOrDefaultAsync(a => a.Name == newName);
                    if (nameExists != null)
                        return null;
                    var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == oldName);
                    author!.Name = newName;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return author;
                }
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine($"Error updating author name: {ex.Message}");
                return null;

            }
        }
    }
}
