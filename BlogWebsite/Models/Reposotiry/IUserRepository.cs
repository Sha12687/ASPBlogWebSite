using Microsoft.AspNetCore.Identity;

namespace BlogWebsite.Models.Reposotiry
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
