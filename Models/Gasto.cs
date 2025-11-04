using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPFINALFINANZAS.Models
{
    // un gasto registrado por un usuario y asociado a una categoria
    public class Gasto
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Descripcion { get; set; } = string.Empty; // texto corto sin puntuacion

        [Range(0.01, 100000000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; } // importe del gasto

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today; // fecha del gasto

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
