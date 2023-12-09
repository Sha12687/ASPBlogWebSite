using BlogWebsite.Models.Reposotiry;
using BlogWebsite.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{


    [Authorize (Roles ="admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUserController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users=await userRepository.GetAll();
            var userView = new UserViewModel();
            userView.Users = new List<User>();
            foreach(var claims in users)
            {
                userView.Users.Add(new Models.ViewModel.User
                {
                    Id = Guid.Parse(claims.Id),
                    Username = claims.UserName,
                    Email = claims.Email,
                });
            }
            return View(userView);
        }
        [HttpPost]
        public async Task <IActionResult> Index(UserViewModel userView) {
            var identityUser = new IdentityUser
            {
                UserName = userView.Username,
                Email = userView.Email,

            };
            var identityResult=await userManager.CreateAsync(identityUser, userView.Password);
            if(identityResult is  not null)
            {
                if(identityResult.Succeeded)
                {
                    // assign role ti this user
                    var roles = new List<string> { "User" };
                    if(userView.AdminRoleCheckBox)
                    {
                        roles.Add("Admin");
                    }
                identityResult= await userManager.AddToRolesAsync(identityUser, roles);
                    if(identityResult is not null && identityResult.Succeeded) {
                        return RedirectToAction("Index", "AdminUser");
                    }
                }
            }
            return View();
        }
    }
}
