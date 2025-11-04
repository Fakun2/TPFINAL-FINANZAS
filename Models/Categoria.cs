using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.Models
{
    // permite clasificar los gastos por ejemplo comida transporte salud
    public class Categoria
    {
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Nombre { get; set; } = string.Empty;

        public bool Activa { get; set; } = true; // bandera para activar o desactivar

        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}
