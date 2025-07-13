using Microsoft.AspNetCore.Mvc;
using Tarius.Data;
using Tarius.Models;

namespace Tarius.Controllers.Usuarios
{
    public class SingInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SingInController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SingIn()
        {
            return View("~/Views/Usuarios/SingIn.cshtml");
        }

        // Procesa el formulario de registro
        [HttpPost]

        public IActionResult SingIn(Tarius.Models.Usuarios nuevoUsuario)
        {
            if (!ModelState.IsValid)
            {
                // Asignar automáticamente el rol "Cliente"
                nuevoUsuario.Rol = "Cliente";

                _context.Usuarios.Add(nuevoUsuario);
                _context.SaveChanges();

                // Redireccionar al login
                return RedirectToAction("Login", "Login");
            }

            return View("~/Views/Usuarios/SingIn.cshtml", nuevoUsuario); 
        }
    }
}
