using BlogWebsite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogWebsite.Models.Reposotiry
{
    public class UserRepository : IUserRepository

    {
        private readonly AuthDbContext authDbContext;

        public UserRepository( AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();
         var adminUser = users.FirstOrDefault(u=>u.UserName== "superAdmin@bloggie.com");
            if(adminUser is not null)
            {
                users.Remove(adminUser);
            }
            return users;
        }
    }
}
