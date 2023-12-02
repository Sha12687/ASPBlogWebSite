using BlogWebsite.Data;
using BlogWebsite.Models.Domain;
using BlogWebsite.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Models.Reposotiry
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext blogDbContext;
        public TagRepository(BlogDbContext dbContext)
        {
            blogDbContext = dbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {

            await blogDbContext.Tags.AddAsync(tag);
            await blogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var deleteTag = await blogDbContext.Tags.FindAsync(id);
            if(deleteTag != null)
            {
                blogDbContext.Tags.Remove(deleteTag);
                await blogDbContext.SaveChangesAsync();
                return deleteTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> getAllAsync()
        {
            List<Tag> tagList = await blogDbContext.Tags.ToListAsync();
            await blogDbContext.SaveChangesAsync();
            return tagList;
        }

        public async Task<Tag> GetAsync(Guid id)
        {
          var existingTag= await blogDbContext.Tags.FindAsync(id);

            return existingTag;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var ExistingTag = await blogDbContext.Tags.FindAsync(tag.Id);
           
            if (ExistingTag != null)
            {
                ExistingTag.Name = tag.Name;
                ExistingTag.DisplayName = tag.DisplayName;
                blogDbContext.Tags.Update(ExistingTag);
                await blogDbContext.SaveChangesAsync();
                return (ExistingTag);
            }
            return null;
        }
    }
}
