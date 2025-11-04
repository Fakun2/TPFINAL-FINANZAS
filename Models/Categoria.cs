using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de categoría válido")]
        [StringLength(50, ErrorMessage = "El nombre de categoría no puede superar los 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Activa")]
        public bool Activa { get; set; }

        // Relación con gastos
        public ICollection<Gasto>? Gastos { get; set; }
    }
}
