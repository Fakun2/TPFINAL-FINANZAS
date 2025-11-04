using Microsoft.AspNetCore.Mvc;
using TPFINALFINANZAS.Models;
using TPFINALFINANZAS.Repositories;

namespace TPFINALFINANZAS.Controllers
{
    // controlador para CRUD de categorias
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepositorio _repo;
        public CategoriasController(ICategoriaRepositorio repo) { _repo = repo; }

        public async Task<IActionResult> Index()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return View(lista);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria modelo)
        {
            if (!ModelState.IsValid) return View(modelo);
            await _repo.AgregarAsync(modelo);
            await _repo.GuardarAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Categoria modelo)
        {
            if (id != modelo.Id) return BadRequest();
            if (!ModelState.IsValid) return View(modelo);
            var entidad = await _repo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            entidad.Nombre = modelo.Nombre;
            entidad.Activa = modelo.Activa;
            _repo.Actualizar(entidad);
            await _repo.GuardarAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            return View(entidad);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var entidad = await _repo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            _repo.Eliminar(entidad);
            await _repo.GuardarAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
