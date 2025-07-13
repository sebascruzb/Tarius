using Microsoft.EntityFrameworkCore;
using Tarius.Models;
using Tarius.Models.Cliente;
using Tarius.Models.Colaborador;

namespace Tarius.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<PasoReceta> PasoRecetas { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<RecetaPlan> RecetasPlan { get; set; }
        public DbSet<IngredienteCliente> IngredienteCliente { get; set; }
        public DbSet<ListaComprasCliente> ListaComprasCliente { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=sqlTarius;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }
    }
}