using TestApplication.Models;

namespace TestApplication.Interfaces
{
    public interface IAuthorService
    {
        Task<Author?> GetAuthorByName(string name);
        Task<Author> CreateAuthor(string name);
        Task<Author?> UpdateAuthorName(string oldName, string newName);
        Task<bool> DeleteAuthorByName(string name);
        Task<List<Author>> GetAllAuthors();
    }
}
