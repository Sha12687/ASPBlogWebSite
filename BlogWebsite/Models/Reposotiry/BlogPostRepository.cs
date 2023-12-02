using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace BlogWebsite.Models.Reposotiry
{
    public class BlogPostRepository : IBlogPostRepository
    {
       private readonly  BlogDbContext BlogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext)
        {
            this.BlogDbContext = blogDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
               await BlogDbContext.BlogPosts.AddAsync(blogPost);
                BlogDbContext.SaveChanges();
            return blogPost;

        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existingBlog =await BlogDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
              BlogDbContext.BlogPosts.Remove(existingBlog);
              await  BlogDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
          return   await BlogDbContext.BlogPosts.Include(x=>x.tags).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
        BlogPost blogPost= await BlogDbContext.BlogPosts.Include(x=>x.tags).FirstOrDefaultAsync(x=>x.Id==id);
            return blogPost;
        }

        public async Task<BlogPost> GetByUrlHandleAsync(string urlHandle)
        {
        var Tempmodel=  await BlogDbContext.BlogPosts.Include(x=>x.tags).FirstOrDefaultAsync(x=> x.UrlHandle == urlHandle);
            return Tempmodel;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
        var existingBlogPost=  await BlogDbContext.BlogPosts.Include(x =>x.tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if(existingBlogPost!=null)
            {
            existingBlogPost.Id=blogPost.Id;
            existingBlogPost.Heading=blogPost.Heading;
            existingBlogPost.PageTitle=blogPost.PageTitle;
            existingBlogPost.Content=blogPost.Content;
            existingBlogPost.ShortDescription=blogPost.ShortDescription;
            existingBlogPost.FeaturedIamgeUrl=blogPost.FeaturedIamgeUrl;
            existingBlogPost.Author=blogPost.Author;
            existingBlogPost.UrlHandle=blogPost.UrlHandle;
                existingBlogPost.Visible=blogPost.Visible;
                existingBlogPost.PublishedDate=blogPost.PublishedDate;
              await  BlogDbContext.SaveChangesAsync();

                return blogPost;
            }
            return null;
        }

    }
}
