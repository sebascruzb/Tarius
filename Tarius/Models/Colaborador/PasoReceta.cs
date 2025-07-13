namespace Tarius.Models.Colaborador
{
    public class PasoReceta
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string? Descripcion { get; set; }

        public int RecetaId { get; set; }
        public Receta Receta { get; set; } = new();
    }

}
