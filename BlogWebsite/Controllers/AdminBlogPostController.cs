using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using BlogWebsite.Models.Reposotiry;
using BlogWebsite.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebsite.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminBlogPostController : Controller
    {

        private readonly ITagRepository TagRepository; 
        private readonly IBlogPostRepository IBlogPostRepository;
        public AdminBlogPostController(ITagRepository tagRepository,IBlogPostRepository blogPostRepository)
        {
           this.TagRepository = tagRepository;
            this.IBlogPostRepository = blogPostRepository;
        }

    

        public async Task<IActionResult> Add()
        {
            var tags= await TagRepository.getAllAsync();
            BlogPostModel blogPostModel = new BlogPostModel { 
           Tags=tags.Select(x => new SelectListItem {Text=x.Name,Value=x.Id.ToString() } )
            };
            return View(blogPostModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BlogPostModel blogPostModel)
        {
            BlogPost blog = new BlogPost
            {
                Author = blogPostModel.Author,
                Content = blogPostModel.Content,
                FeaturedIamgeUrl = blogPostModel.FeaturedIamgeUrl,
                Heading = blogPostModel.Heading,
                PageTitle = blogPostModel.PageTitle,
                PublishedDate = blogPostModel.PublishedDate,
                ShortDescription = blogPostModel.ShortDescription,
                Visible= blogPostModel.Visible,
                UrlHandle = blogPostModel.UrlHandle,
            };
            // Map tags from selected tags
            var selectedTag = new List<Tag>(); 
            foreach(var item in blogPostModel.SelectedTags)
            {
                var selectedTags = Guid.Parse(item);
                Tag  existingTag = await TagRepository.GetAsync(selectedTags);
                if (existingTag != null)
                {
                    selectedTag.Add(existingTag);

                }
            }
            // Mapping tags with doamin model
            blog.tags = selectedTag;
        await   this.IBlogPostRepository.AddAsync(blog);  
            return View(blogPostModel);
        }
        [HttpGet]
        [ActionName("BlogPosts")]
        public async Task<IActionResult> GetAllBlogs()
        {
        var blogPosts=    await this.IBlogPostRepository.GetAllAsync();
        //   List<BlogPostModel> blogPostsModelList = new List<BlogPostModel>();
            //foreach (var blogPostModel in blogPosts)
            //{
            //    BlogPostModel blog = new BlogPostModel
            //    {
            //        Id = blogPostModel.Id,
            //        Author = blogPostModel.Author,
            //        Content = blogPostModel.Content,
            //        FeaturedIamgeUrl = blogPostModel.FeaturedIamgeUrl,
            //        Heading = blogPostModel.Heading,
            //        PageTitle = blogPostModel.PageTitle,
            //        PublishedDate = blogPostModel.PublishedDate,
            //        ShortDescription = blogPostModel.ShortDescription,
            //        Visible = blogPostModel.Visible,
            //        UrlHandle = blogPostModel.UrlHandle,
                   
            //    };
            //    blogPostsModelList.Add(blog);
            //}
            return View(blogPosts);
        }
        [HttpGet]
        [ActionName("EditBlog")]
        public async Task<IActionResult> EditPostBlog(Guid id)
        {
            // first we retrive the resultfrom the repository
          var blog= await IBlogPostRepository.GetAsync(id);
            var tagDomainModel = await TagRepository.getAllAsync();
            if (blog != null)
            {
                BlogPostModel blogModel = new BlogPostModel
                {
                    Id = blog.Id,
                    Author = blog.Author,
                    Content = blog.Content,
                    FeaturedIamgeUrl = blog.FeaturedIamgeUrl,
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    PublishedDate = blog.PublishedDate,
                    ShortDescription = blog.ShortDescription,
                    Visible = blog.Visible,
                    UrlHandle = blog.UrlHandle,
                    Tags = tagDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    SelectedTags = blog.tags.Select(x => x.Id.ToString()).ToArray()

                };
                return View(blogModel);
            }
            return View(null);
        }
        [HttpPost]
        [ActionName("EditBlog")]
        public async Task<IActionResult> EditPostBlog(BlogPostModel blogPostModel)
        {
            // first we retrive the resultfrom the repository
                BlogPost blogModel = new BlogPost
                {
                    Id = blogPostModel.Id,
                    Author = blogPostModel.Author,
                    Content = blogPostModel.Content,
                    FeaturedIamgeUrl = blogPostModel.FeaturedIamgeUrl,
                    Heading = blogPostModel.Heading,
                    PageTitle = blogPostModel.PageTitle,
                    PublishedDate = blogPostModel.PublishedDate,
                    ShortDescription = blogPostModel.ShortDescription,
                    Visible = blogPostModel.Visible,
                    UrlHandle = blogPostModel.UrlHandle,
                };
            List<Tag> tags = new List<Tag>();
            foreach(var Selectedtags in blogPostModel.SelectedTags)
            {
                var selectedTags = Guid.Parse(Selectedtags);
                Tag existingTag= await TagRepository.GetAsync(selectedTags);
                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
            }
            blogModel.tags= tags;   
       var updatedBlogPost =  await   IBlogPostRepository.UpdateAsync(blogModel);
            if(updatedBlogPost != null)
            {
                // show success notification 

                return RedirectToAction("EditBlog");

            }
            else
            {
                // show error notification
                return RedirectToAction("EditBlog");
            }
           
        }

        [HttpPost]
        [ActionName("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(BlogPostModel blogPostModel)
        {
            // first we talked to the repository to delete the post and tags
            var deleteBlogPost = await IBlogPostRepository.DeleteAsync(blogPostModel.Id);

            // then display the response
            if (deleteBlogPost != null)
            {
                // show success notification 

                return RedirectToAction("BlogPosts");

            }
            else
            {
                // show error notification
                return RedirectToAction("EditBlog", new {id=blogPostModel.Id});
            }
           
        }
        }
    }
