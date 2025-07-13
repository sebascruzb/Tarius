using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Colaborador;

namespace Tarius.Controllers.Usuarios.Colaborador
{
    [Authorize(Roles = "Colaborador")]
    public class DashboardColaboradorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardColaboradorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Menu()
        {
            return View("~/Views/Usuarios/Colaborador/Menu.cshtml");
        }



        public IActionResult ListaRecetas()
        {
            var recetas = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .ToList();

            return View("~/Views/Usuarios/Colaborador/ListaRecetas/ListaRecetas.cshtml", recetas);

        }


        public IActionResult Planes()
        {
            var planes = _context.Planes.Include(p => p.RecetasPlan).ThenInclude(rp => rp.Receta).ToList();
            return View("~/Views/Usuarios/Colaborador/Planes/Planes.cshtml", planes);
        }

    }
}
