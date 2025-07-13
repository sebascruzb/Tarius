using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarius.Data;
using Tarius.Models.Cliente;
using Tarius.Models;

namespace Tarius.Controllers.Usuarios.Cliente
{
    public class MisIngredientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MisIngredientesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Ingredientes()
        {
            // 1. Obtener el correo desde los claims
            var correoUsuario = User.Identity?.Name;

            if (string.IsNullOrEmpty(correoUsuario))
            {
                return RedirectToAction("Login", "Login");
            }

            // 2. Obtener el ID del usuario desde la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correoUsuario);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Correo = correoUsuario;


            // 3. Filtrar los ingredientes por el ID del usuario
            var ingredientes = await _context.IngredienteCliente
                .Include(i => i.Usuario)
                .Where(i => i.UsuarioId == usuario.Id)
                .ToListAsync();
             
            return View("~/Views/Usuarios/Cliente/MisIngredientes/Ingredientes.cshtml", ingredientes);
        }

        //Get del Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View("~/Views/Usuarios/Cliente/MisIngredientes/Crear.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(IngredienteCliente ingrediente)
        {
            var correoUsuario = User.Identity?.Name;
            if (string.IsNullOrEmpty(correoUsuario))
                return RedirectToAction("Login", "Login");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correoUsuario);
            if (usuario == null)
                return RedirectToAction("Login", "Login");

            if (!ModelState.IsValid)
            {
                ingrediente.UsuarioId = usuario.Id;
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();

                // Redirige a la acción que carga la vista con la lista de ingredientes
                return RedirectToAction("Ingredientes", "MisIngredientes");
            }

            return View("~/Views/Usuarios/Cliente/MisIngredientes/Crear.cshtml", ingrediente);
        }


        // Get del Editar
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var ingrediente = await _context.IngredienteCliente.FindAsync(id);
            if (ingrediente == null) return NotFound();

            return View("~/Views/Usuarios/Cliente/MisIngredientes/Editar.cshtml", ingrediente);
        }


        //Post Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, string Cantidad)
        {
            var ingrediente = await _context.IngredienteCliente.FindAsync(id);
            if (ingrediente == null) return NotFound();

            if (string.IsNullOrWhiteSpace(Cantidad))
            {
                ViewBag.Mensaje = "La cantidad no puede estar vacía.";
                return View("~/Views/Usuarios/Cliente/MisIngredientes/Editar.cshtml", ingrediente);
            }

            ingrediente.Cantidad = Cantidad;

            _context.Update(ingrediente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Ingredientes", "MisIngredientes");
        }

        //get eliminar
        [HttpGet]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var ingrediente = await _context.IngredienteCliente.FindAsync(id);
            if (ingrediente == null) return NotFound();

            return View("~/Views/Usuarios/Cliente/MisIngredientes/Eliminar.cshtml", ingrediente);
        }


        //Post eliminar
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var ingrediente = await _context.IngredienteCliente.FindAsync(id);
            if (ingrediente == null) return NotFound();

            _context.IngredienteCliente.Remove(ingrediente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Ingredientes", "MisIngredientes");
        }




    }
}