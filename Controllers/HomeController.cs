using Microsoft.AspNetCore.Mvc;

namespace TPFINALFINANZAS.Controllers
{
    // controlador principal de la pagina de inicio
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
