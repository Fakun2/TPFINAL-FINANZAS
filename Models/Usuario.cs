using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.Models
{
    // representa una persona que registra gastos
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Nombre { get; set; } = string.Empty; // nombre visible en listas

        [EmailAddress, StringLength(120)]
        public string? Email { get; set; } // email opcional

        // relacion uno a muchos  un usuario puede tener varios gastos
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}
