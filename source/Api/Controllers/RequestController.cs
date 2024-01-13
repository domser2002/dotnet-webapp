using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
