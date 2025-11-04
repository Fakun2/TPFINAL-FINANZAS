using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPFINALFINANZAS.Models;
using TPFINALFINANZAS.Repositories;

namespace TPFINALFINANZAS.Controllers
{
    public class GastosController : Controller
    {
        private readonly IGastoRepositorio _gastoRepo;
        private readonly ICategoriaRepositorio _catRepo;
        private readonly IUsuarioRepositorio _userRepo;

        public GastosController(IGastoRepositorio gastoRepo, ICategoriaRepositorio catRepo, IUsuarioRepositorio userRepo)
        {
            _gastoRepo = gastoRepo;
            _catRepo = catRepo;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _gastoRepo.ObtenerConTodoAsync();
            return View(lista);
        }

        public async Task<IActionResult> Crear()
        {
            await CargarCombos();
            return View(new Gasto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Gasto modelo)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(modelo);
            }
            await _gastoRepo.AgregarAsync(modelo);
            await _gastoRepo.GuardarAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var entidad = await _gastoRepo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            await CargarCombos();
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Gasto modelo)
        {
            if (id != modelo.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(modelo);
            }
            var entidad = await _gastoRepo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();

            entidad.Descripcion = modelo.Descripcion;
            entidad.Monto = modelo.Monto;
            entidad.Fecha = modelo.Fecha;
            entidad.CategoriaId = modelo.CategoriaId;
            entidad.UsuarioId = modelo.UsuarioId;

            _gastoRepo.Actualizar(entidad);
            await _gastoRepo.GuardarAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await _gastoRepo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            return View(entidad);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                var entidad = await _gastoRepo.ObtenerPorIdAsync(id);
                if (entidad == null) return NotFound();

                _gastoRepo.Eliminar(entidad);
                await _gastoRepo.GuardarAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar gasto: {ex.Message}");
                return BadRequest("Error interno al intentar eliminar el gasto.");
            }
        }

        private async Task CargarCombos()
        {
            var cats = await _catRepo.ObtenerTodosAsync();
            var users = await _userRepo.ObtenerTodosAsync();
            ViewData["CategoriaId"] = new SelectList(cats, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(users, "Id", "Nombre");
        }
    }
}
