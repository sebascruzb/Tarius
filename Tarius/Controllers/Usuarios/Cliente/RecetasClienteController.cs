using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Cliente;
using Tarius.Models.Colaborador; // necesario para acceder al modelo Receta

namespace Tarius.Controllers.Usuarios.Cliente
{
    public class RecetasClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecetasClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> VerRecetas()
        {
            var recetas = await _context.Recetas.ToListAsync();
            return View("~/Views/Usuarios/Cliente/Recetas/VerRecetas.cshtml", recetas);
        }


        [HttpGet]
        public async Task<IActionResult> SeleccionarPorciones(int recetaId)
        {
            var receta = await _context.Recetas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == recetaId);

            if (receta == null)
                return NotFound();

            ViewBag.RecetaId = recetaId;
            ViewBag.NombreReceta = receta.Nombre;

            return View("~/Views/Usuarios/Cliente/Recetas/SeleccionarPorciones.cshtml");
        }


        //ver lista de compras
        public async Task<IActionResult> ListaCompras()
        {
            var correoUsuario = User.Identity?.Name;
            if (string.IsNullOrEmpty(correoUsuario))
                return RedirectToAction("Login", "Login");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correoUsuario);
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var lista = await _context.ListaComprasCliente
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();

            return View("~/Views/Usuarios/Cliente/ListaCompras/ListaCompras.cshtml", lista);
        }






        //logica de comparacion de los inghredientes de la receta con los ingredientes del stock, multiplicando por porciones.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarPorciones(int recetaId, int porciones)
        {
            var correoUsuario = User.Identity?.Name;
            if (string.IsNullOrEmpty(correoUsuario))
                return RedirectToAction("Login", "Login");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correoUsuario);
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            var receta = await _context.Recetas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == recetaId);

            if (receta == null || receta.Ingredientes == null || !receta.Ingredientes.Any())
                return RedirectToAction("VerRecetas");

            var inventario = await _context.IngredienteCliente
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();

            foreach (var ingredienteReceta in receta.Ingredientes)
            {
                // Convertir la cantidad de la receta a double
                if (!double.TryParse(ingredienteReceta.Cantidad, out double cantidadBase))
                    continue; // Saltar si la cantidad no es numérica válida

                double cantidadNecesaria = cantidadBase * porciones;

                var enInventario = inventario.FirstOrDefault(i =>
                    i.Nombre?.Trim().ToLower() == ingredienteReceta.Nombre?.Trim().ToLower() &&
                    i.Unidad?.Trim().ToLower() == ingredienteReceta.Unidad?.Trim().ToLower());

                double cantidadDisponible = 0;

                if (enInventario != null && double.TryParse(enInventario.Cantidad, out double cantidadParseada))
                    cantidadDisponible = cantidadParseada;

                if (cantidadDisponible < cantidadNecesaria)
                {
                    double cantidadFaltante = cantidadNecesaria - cantidadDisponible;

                    _context.ListaComprasCliente.Add(new ListaComprasCliente
                    {
                        UsuarioId = usuario.Id,
                        NombreIngrediente = ingredienteReceta.Nombre!,
                        Unidad = ingredienteReceta.Unidad!,
                        Cantidad = Math.Round(cantidadFaltante, 2)
                    });
                }
            }

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Ingredientes faltantes añadidos a la lista de compras.";

            return RedirectToAction("VerRecetas");
        }




    }
}
