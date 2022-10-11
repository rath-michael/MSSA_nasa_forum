using Microsoft.AspNetCore.Mvc;

namespace Week15Project.Services
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
