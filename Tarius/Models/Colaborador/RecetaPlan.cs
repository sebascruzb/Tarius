using Tarius.Models.Colaborador;

namespace Tarius.Models.Colaborador
{
    public class RecetaPlan
    {
        public int Id { get; set; }

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = new();

        public int RecetaId { get; set; }
        public Receta Receta { get; set; } = new();
    }

}
