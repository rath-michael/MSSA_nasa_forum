using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Week15Project.Models;
using Week15Project.Models.ViewModels;
using Week15Project.Services;

namespace Week15Project.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IForumRepository repository;

        public AccountController(SignInManager<User> _signInManager, UserManager<User> _userManager,
            RoleManager<IdentityRole> _roleManager, IForumRepository forumRepository)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            repository = forumRepository;
        }

        public IActionResult Login()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password, bool RememberMe)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Username, Password, RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", errorMessage: "Login unsuccessful");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User()
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    UserName = registerViewModel.UserName,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    Email = registerViewModel.Email
                };

                var result = await userManager.CreateAsync(newUser, registerViewModel.Password);

                if (result.Succeeded)
                {
                    if (newUser.UserName == "Admin")
                    {
                        await userManager.AddToRoleAsync(newUser, "Admin");
                        await userManager.AddToRoleAsync(newUser, "User");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser, "User");
                    }
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> GetUserProfile()
        {
            User currentUser = await userManager.GetUserAsync(User);

            UserViewModel model = new UserViewModel()
            {
                User = currentUser,
                Posts = repository.GetPostsByUser(currentUser.Id),
                Responses = repository.GetResponsesByUser(currentUser.Id)
            };
            
            return View(model);

        }

        public IActionResult ViewUserProfile(string id)
        {
            var user = repository.GetUser(id);
            return View(user);
        }
    }
}
