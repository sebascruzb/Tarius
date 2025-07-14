using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Tarius.Models.Colaborador
{
    public class PasoReceta
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string? Descripcion { get; set; }

        public int RecetaId { get; set; }

        [JsonIgnore]
        public Receta? Receta { get; set; }
    }

}
