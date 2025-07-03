using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
