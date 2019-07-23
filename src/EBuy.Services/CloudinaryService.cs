namespace EBuy.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Contracts;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadImage(IFormFile pictureFile, string fileName)
        {
            byte[] destination;

            using (var stream = new MemoryStream())
            {
                await pictureFile.CopyToAsync(stream);
                destination = stream.ToArray();
            }

            UploadResult uploadResult;

            using (var stream = new MemoryStream(destination))
            {
                var uploadParams = new ImageUploadParams
                {
                    Folder = "product_images",
                    File = new FileDescription(fileName, stream)
                };

                uploadResult = this.cloudinary.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
