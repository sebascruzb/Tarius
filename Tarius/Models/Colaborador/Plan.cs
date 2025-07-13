namespace Tarius.Models.Colaborador
{
    public class Plan
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Objetivo { get; set; } // bajar de peso, ganar masa...

        public List<RecetaPlan> RecetasPlan { get; set; } = new();
    }



}
