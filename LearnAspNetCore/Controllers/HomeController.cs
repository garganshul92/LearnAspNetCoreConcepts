using Microsoft.AspNetCore.Mvc;

namespace LearnAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Index()
        {
            return Json(new {id = 1, Name = "Anshul"});
        }
    }
}
