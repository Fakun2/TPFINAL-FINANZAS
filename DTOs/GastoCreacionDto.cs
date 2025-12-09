using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.DTOs
{
    public class GastoCreacionDto
    {
        // 1. Inicialización de Fecha a HOY (DateTime.Now.Date)
        // Usar Date para evitar la hora si el input es de tipo date
        public DateTime Fecha { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "Debe ingresar una descripción del gasto")]
        [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar un monto válido")]
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }
    }
}