using TestApplication.Models;

namespace TestApplication.Interfaces
{
    public interface IGenreService
    {
        Task<Genre?> GetGenreByName(string name);
        Task<Genre?> CreateGenre(string name);
        Task<Genre?> UpdateGenreName(string oldName, string newName);
        Task<bool> DeleteGenreByName(string name);
        Task<List<Genre>> GetAllGenres(int pageNumber, int pageSize);
    }
}
