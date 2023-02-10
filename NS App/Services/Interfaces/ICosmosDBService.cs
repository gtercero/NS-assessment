using NS_App.Entities;
using NS_App.Models;

namespace NS_App.Services.Interfaces
{
    public interface ICosmosDBService
    {
        Task<IEnumerable<FileViewModel>> GetAsync();

        Task AddAsync(Metadata item);

        Task DeleteAsync(string id);
    }
}
