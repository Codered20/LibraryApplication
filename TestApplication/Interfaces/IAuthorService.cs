using TestApplication.Models;

namespace TestApplication.Interfaces
{
    public interface IAuthorService
    {
        Task<Author?> GetAuthorByName(string name);
        Task<Author> CreateAuthor(string name);
        Task<Author?> UpdateAuthorName(string oldName, string newName);
        Task<int> DeleteAuthorByName(string name);
        Task<List<Author>> GetAllAuthors(int pageNumber, int pageSize);
    }
}
