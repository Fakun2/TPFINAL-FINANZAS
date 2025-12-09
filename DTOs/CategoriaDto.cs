using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de Categoría")]
        public string Nombre { get; set; } = string.Empty;

        // Puedes agregar aquí cualquier otro campo que necesites mostrar,
        // pero que no quieras exponer directamente desde la entidad de EF.
    }
}