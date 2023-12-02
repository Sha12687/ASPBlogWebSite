using BlogWebsite.Models.Reposotiry;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
    public class BLogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BLogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]   
        public async Task<IActionResult> Index(string UrlHandle)
        {
      var blogPost =   await blogPostRepository.GetByUrlHandleAsync(UrlHandle);
            return View(blogPost);
        }
    }
}
