using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NS_App.Models;
using NS_App.Entities;
using NS_App.Services.Interfaces;

namespace NS_App.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public BlobStorageService(string storageConnectionString, string storageContainerName)
        {
            _storageConnectionString = storageConnectionString;
            _storageContainerName = storageContainerName;
        }
        public async Task UploadFileAsync(IFormFile files, string id)
        {
            try
            {
                string blobFileName = files.FileName + id;
                // Retrieve storage account from connection string.
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                // Create the blob client.
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container.
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_storageContainerName);

                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(blobFileName);
                await using (var data = files.OpenReadStream())
                {

                    await blockBlob.UploadFromStreamAsync(data);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<DownloadViewModel> DownloadFileAsync(string BlobName)
        {
            try
            {
                CloudBlockBlob blockBlob;
                await using (MemoryStream memoryStream = new MemoryStream())
                {

                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_storageContainerName);
                    blockBlob = cloudBlobContainer.GetBlockBlobReference(BlobName);
                    await blockBlob.DownloadToStreamAsync(memoryStream);
                }
                Stream blobStream = blockBlob.OpenReadAsync().Result;

                DownloadViewModel downloadedFile = new DownloadViewModel
                {
                    blobStream = blobStream,
                    ContentType = blockBlob.Properties.ContentType,
                    Name = blockBlob.Name
                };

                return downloadedFile;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteFileAsync(string blobName)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_storageConnectionString);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_storageContainerName);
                var blob = cloudBlobContainer.GetBlobReference(blobName);
                await blob.DeleteIfExistsAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
