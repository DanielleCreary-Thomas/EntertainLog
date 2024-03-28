using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
