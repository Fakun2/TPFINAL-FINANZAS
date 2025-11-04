using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPFINALFINANZAS.Models
{
    public class Gasto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción del gasto")]
        [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar un monto válido")]
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "Debe ingresar una fecha válida")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}