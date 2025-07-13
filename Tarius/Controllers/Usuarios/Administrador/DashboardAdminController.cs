using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarius.Data;

namespace Tarius.Controllers.Usuarios.Administrador
{
    [Authorize(Roles = "Administrador")]
    public class DashboardAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Vista del dashboard administrador
        public IActionResult Menu()
        {
            var usuarios = _context.Usuarios.ToList();

            return View("~/Views/Usuarios/Administrador/Menu.cshtml", usuarios);
        }

        // Crear usuario
        public IActionResult Create()
        {
            return View("~/Views/Usuarios/Administrador/Crear.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Tarius.Models.Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Menu");
            }

            return View("~/Views/Usuarios/Administrador/Crear.cshtml", usuario);
        }

        // Editar usuario
        public IActionResult Edit(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            return View("~/Views/Usuarios/Administrador/Editar.cshtml", usuario);
        }

        [HttpPost]
        public IActionResult Edit(Tarius.Models.Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction("Menu");
            }

            return View("~/Views/Usuarios/Administrador/Editar.cshtml", usuario);
        }

        // Eliminar usuario
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
            return RedirectToAction("Menu");
        }
    }
}
