using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPFINALFINANZAS.Repositories;
using AutoMapper; // Necesario para usar IMapper
using TPFINALFINANZAS.DTOs; // Necesario para usar los DTOs
using TPFINALFINANZAS.Models; // Sigue siendo necesario para la Entidad en el Repositorio

namespace TPFINALFINANZAS.Controllers
{
    public class GastosController : Controller
    {
        private readonly IGastoRepositorio _gastoRepo;
        private readonly ICategoriaRepositorio _catRepo;
        private readonly IUsuarioRepositorio _userRepo;
        private readonly IMapper _mapper; // <-- 1. Inyectar AutoMapper

        public GastosController(
            IGastoRepositorio gastoRepo,
            ICategoriaRepositorio catRepo,
            IUsuarioRepositorio userRepo,
            IMapper mapper) // <-- 1. Inyectar AutoMapper en el constructor
        {
            _gastoRepo = gastoRepo;
            _catRepo = catRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        // ----------------------------------------------------------------------------------
        // READ (Index) - Devuelve DTOs
        // ----------------------------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            // 1. Obtener Entidades de la BD
            var listaEntidades = await _gastoRepo.ObtenerConTodoAsync();

            // 2. Mapear Entidades a DTOs (Usa GastoDto que incluye nombres de relaciones)
            var listaDto = _mapper.Map<IEnumerable<GastoDto>>(listaEntidades);

            // 3. Devolver lista de DTOs a la vista
            return View(listaDto);
        }

        // ----------------------------------------------------------------------------------
        // CREATE (GET) - Usa DTO de Creación e Inicializa la Fecha
        // ----------------------------------------------------------------------------------
        public async Task<IActionResult> Crear()
        {
            await CargarCombos();
            // 1. Devuelve el DTO de creación, que ya tiene Fecha = Hoy por defecto.
            return View(new GastoCreacionDto());
        }

        // ----------------------------------------------------------------------------------
        // CREATE (POST) - Recibe DTO y lo Mapea a Entidad
        // ----------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(GastoCreacionDto modeloDto) // <-- 2. Recibe DTO
        {
            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(modeloDto);
            }

            // 1. Mapear DTO (Entrada) a Entidad (DB)
            var entidad = _mapper.Map<Gasto>(modeloDto);

            // 2. Usar Repositorio para guardar la Entidad
            await _gastoRepo.AgregarAsync(entidad);
            await _gastoRepo.GuardarAsync();

            return RedirectToAction(nameof(Index));
        }

        // ----------------------------------------------------------------------------------
        // UPDATE (GET) - Convierte Entidad a DTO antes de enviarlo a la vista
        // ----------------------------------------------------------------------------------
        public async Task<IActionResult> Editar(int id)
        {
            var entidad = await _gastoRepo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();

            // 1. Mapear Entidad a DTO de Creación/Actualización (para rellenar el formulario)
            var modeloDto = _mapper.Map<GastoCreacionDto>(entidad);

            await CargarCombos();
            return View(modeloDto); // <-- Devuelve DTO
        }

        // ----------------------------------------------------------------------------------
        // UPDATE (POST) - Recibe DTO, Actualiza Entidad, Guarda
        // ----------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Se recomienda crear un DTO específico para edición (GastoEdicionDto) si la lógica es distinta, 
        // pero por simplicidad, seguimos usando GastoCreacionDto y le agregamos el Id.
        public async Task<IActionResult> Editar(int id, GastoCreacionDto modeloDto)
        {
            // El Id debe pasarse de la ruta al DTO si no está incluido en el DTO
            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(modeloDto);
            }

            var entidad = await _gastoRepo.ObtenerPorIdAsync(id);
            if (entidad is null) return NotFound();

            // 1. Mapear DTO a la Entidad EXISTENTE
            // Opcional: Podrías mapear campo por campo como lo estabas haciendo, pero
            // AutoMapper facilita la actualización:
            _mapper.Map(modeloDto, entidad); // Mapea los campos del DTO a la entidad existente
            entidad.Id = id; // Asegura que el Id sea correcto

            // 2. Usar Repositorio para actualizar la Entidad
            _gastoRepo.Actualizar(entidad);
            await _gastoRepo.GuardarAsync();

            return RedirectToAction(nameof(Index));
        }

        // Las acciones Eliminar y EliminarConfirmado pueden seguir usando la Entidad 
        // internamente, ya que solo la buscan y la borran, sin interactuar con la vista
        // de forma compleja.

        // Mantiene CargarCombos igual
        private async Task CargarCombos()
        {
            // ... tu código de CargarCombos ...
        }
    }
}