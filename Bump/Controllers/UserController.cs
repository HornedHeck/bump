using Data.Repo;
using Microsoft.AspNetCore.Mvc;
using static Bump.Extensions;

namespace Bump.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public RedirectResult Login()
        {
            _userRepo.Login();
            return Redirect("~/User/Profile");
        }

        public RedirectResult Logout()
        {
            _userRepo.Logout();
            return Redirect("~/User/Start");
        }

        public IActionResult Start()
        {
            return View();
        }

        public IActionResult Profile() => _userRepo.GetCurrentUser()
            ?.Run(View) ?? Run(() => new BadRequestResult() as IActionResult);
    }
}