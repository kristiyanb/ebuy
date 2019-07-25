namespace EBuy.Services.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile pictureFile, string fileName);
    }
}
