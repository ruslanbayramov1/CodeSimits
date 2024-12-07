using Microsoft.AspNetCore.Mvc;

namespace CodeSimits.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
