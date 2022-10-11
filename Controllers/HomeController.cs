using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Week15Project.Models;
using Week15Project.Models.ViewModels;
using Week15Project.Services;

namespace Week15Project.Controllers
{
    public class HomeController : Controller
    {
        private IForumRepository repository;

        public HomeController(IForumRepository forumRepository)
        {
            repository = forumRepository;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}