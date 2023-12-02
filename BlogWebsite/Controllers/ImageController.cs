using BlogWebsite.Models.Reposotiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BlogWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IimageRepository iimageRepository;

        public ImageController(IimageRepository iimageRepository)
        {
            this.iimageRepository = iimageRepository;
        }
        public IActionResult Index()
        {
            return Ok("Testing purose");
        }
        [Consumes("multipart/form-data")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadAsync(IFormFile form)
        {
            if (form == null)
            {
                return BadRequest("No file uploaded");
            }

            // Add logging to check if the action method is being called
            Console.WriteLine("UploadAsync action method called");

            var uploadResult = await iimageRepository.UploadAsync(form);

            if (uploadResult == null)
            {
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = uploadResult });
        }

    }
}
