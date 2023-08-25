using Microsoft.AspNetCore.Mvc;

namespace BlueOath.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
