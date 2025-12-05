using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ArtisanShopAPI.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadImageAsync(Stream filestream, string fileName);
        Task<bool> DeleteImageAsync(string fileName);
        string GetImageUrl(string fileName);
    }
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            string connectionString = configuration["AzureStorage:ConnectionString"];
            _containerName = configuration["AzureStorage:ContainerName"];

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
        {
            // Gives a unique filename so there's no conflicts between files
            string uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            BlobClient blobClient = _containerClient.GetBlobClient(uniqueFileName);

            // Upload
            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders
            {
                ContentType = "image/jpg"
            });

            // Return full URL
            return blobClient.Uri.ToString();
        }

        // Delete image
        public async Task<bool> DeleteImageAsync(string fileName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            return await blobClient.DeleteIfExistsAsync();
        }

        // Get the full URL for an image
        public string GetImageUrl(string fileName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            return blobClient.Uri.ToString();
        }
    }
}
