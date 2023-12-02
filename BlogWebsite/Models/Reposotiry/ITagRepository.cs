using BlogWebsite.Models.Domain;

namespace BlogWebsite.Models.Reposotiry
{
    public interface ITagRepository
    {
     Task<IEnumerable<Tag>>  getAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task<Tag> UpdateAsync(Tag tag);
        Task<Tag> DeleteAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);

    }

}
