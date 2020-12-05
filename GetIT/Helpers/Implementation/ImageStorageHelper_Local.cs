using GetIT.Helpers.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Helpers.Implementation
{
    public class ImageStorageHelper_Local : IImageStorageHelper
    {
        public async Task<string> GetImageData(string imageURL)
        {
            Bitmap bitmap = new Bitmap( "..\\Files\\" + imageURL);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imageBytes = stream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);
            return await Task.FromResult(base64String);
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            string[] formats = { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> UploadImagesToStorage(Stream fileStream, string fileName)
        {
            return await Task.FromResult(true);
        }
    }
}
