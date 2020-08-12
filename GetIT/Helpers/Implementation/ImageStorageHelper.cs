using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GetIT.Helpers.Interface;
using GetIT.Utility.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Helpers.Implementation
{
    public class ImageStorageHelper : IImageStorageHelper
    {
        private readonly AzureStorageConfig _storageConfig = null;
        private readonly StorageSharedKeyCredential _storageCredentials = null;

        public ImageStorageHelper(IOptions<AzureStorageConfig> storageConfig)
        {
            _storageConfig = storageConfig.Value;
            _storageCredentials = new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);
        }



        private Uri getBlobUri(string fileName, string containerName)
        {
            return new Uri($"https://{_storageConfig.AccountName}.blob.core.windows.net/{containerName}/{fileName}");
        }

        public async Task<bool> UploadImagesToStorage(Stream fileStream, string fileName)
        {
            Uri blobUri = getBlobUri(fileName, _storageConfig.ImageContainer);

            BlobClient blobClient = new BlobClient(blobUri, _storageCredentials);

            await blobClient.UploadAsync(fileStream);

            return await Task.FromResult(true);

        }


        public bool IsImage(IFormFile file)
        {
            if(file.ContentType.Contains("image"))
            {
                return true;
            }
            string[] formats = { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

    }
}
