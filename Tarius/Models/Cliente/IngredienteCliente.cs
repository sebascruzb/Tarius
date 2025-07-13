using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tarius.Models;

namespace Tarius.Models.Cliente
{
    public class IngredienteCliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        public string? Cantidad { get; set; }

        [Required(ErrorMessage = "La unidad es obligatoria.")]
        public string? Unidad { get; set; }

        // Relación con el modelo Usuarios (clave foránea)
        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; }
    }
}
