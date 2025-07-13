using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Colaborador;

namespace Tarius.Controllers.Usuarios.Colaborador
{
    [Authorize(Roles = "Colaborador")]
    public class RecetasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lista de recetas
        public IActionResult Index()
        {
            var recetas = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .ToList();

            return View("~/Views/Usuarios/Colaborador/ListaRecetas/listaRecetas.cshtml", recetas);
        }

        // GET: Crear
        public IActionResult Crear()
        {
            return View("~/Views/Usuarios/Colaborador/ListaRecetas/Crear.cshtml");
        }

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Receta receta)
        {
            if (!ModelState.IsValid)
            {
                _context.Recetas.Add(receta);
                _context.SaveChanges();
                return RedirectToAction("ListaRecetas", "DashboardColaborador");
            }

            // Si el modelo no es válido, vuelve a la vista Crear
            return View("~/Views/Usuarios/Colaborador/ListaRecetas/Crear.cshtml", receta);
        }


        // GET: Detalle
        public IActionResult Detalle(int id)
        {
            var receta = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .FirstOrDefault(r => r.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            return View("~/Views/Usuarios/Colaborador/ListaRecetas/Detalle.cshtml", receta);
        }





        // GET: Editar
        public IActionResult Editar(int id)
        {
            var receta = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .FirstOrDefault(r => r.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            return View("~/Views/Usuarios/Colaborador/ListaRecetas/Editar.cshtml", receta);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Receta receta)
        {

            if (ModelState.IsValid)
            {
                return View("~/Views/Usuarios/Colaborador/ListaRecetas/Editar.cshtml", receta);
            }

            // Obtener la receta existente desde la base de datos
            var recetaExistente = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .FirstOrDefault(r => r.Id == receta.Id);

            if (recetaExistente == null)
            {
                return NotFound();
            }

            // Eliminar los ingredientes y pasos anteriores
            _context.Ingredientes.RemoveRange(recetaExistente.Ingredientes);
            _context.PasoRecetas.RemoveRange(recetaExistente.Pasos);

            // Actualizar datos principales
            recetaExistente.Nombre = receta.Nombre;
            recetaExistente.Descripcion = receta.Descripcion;
            recetaExistente.TiempoPreparacion = receta.TiempoPreparacion;
            recetaExistente.Tipo = receta.Tipo;
            recetaExistente.Calorias = receta.Calorias;

            // Asignar nuevos ingredientes y pasos
            recetaExistente.Ingredientes = receta.Ingredientes;
            recetaExistente.Pasos = receta.Pasos;

            _context.SaveChanges();

            return RedirectToAction("ListaRecetas", "DashboardColaborador");
        }


        // GET: Eliminar
        public IActionResult Eliminar(int id)
        {
            var receta = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .FirstOrDefault(r => r.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            return View("~/Views/Usuarios/Colaborador/ListaRecetas/Eliminar.cshtml", receta);
        }

        // POST: Confirmar eliminación
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int id)
        {
            var receta = _context.Recetas
                .Include(r => r.Ingredientes)
                .Include(r => r.Pasos)
                .FirstOrDefault(r => r.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            _context.Ingredientes.RemoveRange(receta.Ingredientes);
            _context.PasoRecetas.RemoveRange(receta.Pasos);
            _context.Recetas.Remove(receta);
            _context.SaveChanges();

            return RedirectToAction("ListaRecetas", "DashboardColaborador");
        }



    }
}
