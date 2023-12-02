using BlogWebsite.Models;
using BlogWebsite.Models.Reposotiry;
using BlogWebsite.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Executing Index action");

                var allPostBLogs = await blogPostRepository.GetAllAsync();
                var tags = await tagRepository.getAllAsync();
                var model = new HomeViewModel
                {
                    BlogPosts = allPostBLogs,
                    Tags = tags
                };

                _logger.LogInformation("Index action completed successfully");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the Index action");
                throw; // Re-throw the exception to let the error handling middleware deal with it
            }
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Executing Privacy action");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Executing Error action");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
