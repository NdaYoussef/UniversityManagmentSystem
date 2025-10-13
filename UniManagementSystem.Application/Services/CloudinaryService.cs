using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.Interfaces;

namespace UniManagementSystem.Application.Services
{
    
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloundinary;
        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloundinary = cloudinary;
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "UserProfiles"
            };

            var result = await _cloundinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
