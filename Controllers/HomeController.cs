using Microsoft.AspNetCore.Mvc;
using TPFINALFINANZAS.Repositories;

namespace TPFINALFINANZAS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGastoRepositorio _gastoRepo;

        public HomeController(IGastoRepositorio gastoRepo)
        {
            _gastoRepo = gastoRepo;
        }

        public async Task<IActionResult> Index()
        {
            var gastos = await _gastoRepo.ObtenerConTodoAsync();
            var total = gastos.Sum(g => g.Monto);
            var porCategoria = gastos
                .GroupBy(g => g.Categoria?.Nombre)
                .Select(c => new { Categoria = c.Key, Total = c.Sum(g => g.Monto) })
                .OrderByDescending(c => c.Total)
                .ToList();

            ViewData["Title"] = "FINANZAS"; // ✅ Agregado para que se muestre el título correcto
            ViewData["Total"] = total;
            ViewData["PorCategoria"] = porCategoria;

            return View();
        }
    }
}