using Microsoft.AspNetCore.Mvc;

namespace QLBGX.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Chart()
        {
            return View();
        }

    }
}
