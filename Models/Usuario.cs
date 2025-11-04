using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar un nombre válido")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar un correo electrónico")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico correcto")]
        public string Email { get; set; } = string.Empty;

        // Relación con gastos
        public ICollection<Gasto>? Gastos { get; set; }
    }
}
