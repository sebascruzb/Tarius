using System.Text.Json.Serialization;

namespace Tarius.Models.Colaborador
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cantidad { get; set; }
        public string? Unidad { get; set; }

        public int RecetaId { get; set; }

        [JsonIgnore]
        public Receta? Receta { get; set; }
    }

}
