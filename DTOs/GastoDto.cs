using System.ComponentModel.DataAnnotations;

namespace TPFINALFINANZAS.DTOs
{
    public class GastoDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public decimal Monto { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        // Propiedades de navegación (para mostrar el nombre en el Index/Detalles)
        [Display(Name = "Categoría")]
        public string NombreCategoria { get; set; } = string.Empty;

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;
    }
}