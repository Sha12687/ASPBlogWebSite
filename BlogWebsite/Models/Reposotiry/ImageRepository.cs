using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Models.Reposotiry
{
    public class ImageRepository : IimageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public ImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account=new Account(configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                  configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
      
        public async Task<string> UploadAsync(IFormFile formFile)
        {
            var clinet = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(formFile.FileName, formFile.OpenReadStream()),
                DisplayName = formFile.FileName
            };
        var uploadResult = await clinet.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;    
    }
    }
}
