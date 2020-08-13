using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GetIT.Helpers.Interface
{
   public interface IImageStorageHelper
    {
        Task<bool> UploadImagesToStorage(Stream fileStream, string fileName);
        bool IsImage(IFormFile file);
        Task<string> GetImageData(string imageURL);
    }
}
