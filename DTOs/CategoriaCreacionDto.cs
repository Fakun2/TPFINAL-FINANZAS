using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.DTOs
{
    public class CategoriaCreacionDto
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        [Display(Name = "Nombre de Categoría")]
        public string Nombre { get; set; } = string.Empty;
    }
}