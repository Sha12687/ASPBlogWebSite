namespace BlogWebsite.Models.Reposotiry
{
    public interface IimageRepository
    {
        public Task<string> UploadAsync(IFormFile formFile);
    }
}
