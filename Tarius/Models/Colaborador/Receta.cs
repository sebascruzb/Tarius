using System.ComponentModel.DataAnnotations;

namespace Tarius.Models.Colaborador
{
    public class Receta
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public int TiempoPreparacion { get; set; } // minutos

        public string? Tipo { get; set; } // "Desayuno", "Almuerzo", "Merienda"

        public int Calorias { get; set; } // Por porción

        public List<Ingrediente> Ingredientes { get; set; } = new();

        public List<PasoReceta> Pasos { get; set; } = new();
    }

}
