using NS_App.Models;
using NS_App.Entities;

namespace NS_App.Services.Interfaces
{
    public interface IBlobStorageService
    {

        Task UploadFileAsync(IFormFile files, string id);
        Task<DownloadViewModel> DownloadFileAsync(string blobName);
        Task DeleteFileAsync(string blobName);
    }
}
