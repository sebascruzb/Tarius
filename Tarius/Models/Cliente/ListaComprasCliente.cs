using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tarius.Models;

namespace Tarius.Models.Cliente
{
    public class ListaComprasCliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreIngrediente { get; set; }

        [Required]
        public string Unidad { get; set; }

        [Required]
        public double Cantidad { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; }
    }
}
