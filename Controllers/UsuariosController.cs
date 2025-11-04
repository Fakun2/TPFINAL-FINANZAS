using Microsoft.AspNetCore.Mvc;
using TPFINALFINANZAS.Models;
using TPFINALFINANZAS.Repositories;

namespace TPFINALFINANZAS.Controllers
{
    // controlador para CRUD de usuarios
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _repo;
        public UsuariosController(IUsuarioRepositorio repo) { _repo = repo; }

        public async Task<IActionResult> Index()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return View(lista);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Usuario modelo)
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
        public async Task<IActionResult> Editar(int id, Usuario modelo)
        {
            if (id != modelo.Id) return BadRequest();
            if (!ModelState.IsValid) return View(modelo);
            var entidad = await _repo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();
            entidad.Nombre = modelo.Nombre;
            entidad.Email = modelo.Email;
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
