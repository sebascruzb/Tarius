using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tarius.Data;

namespace Tarius.Controllers.Usuarios
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Muestra el formulario de login
        public IActionResult Login()
        {
            return View("~/Views/Usuarios/Login.cshtml");
        }


        // Procesa el formulario de login
        [HttpPost]
        public async Task<IActionResult> Login(Tarius.Models.Usuarios Usuario)
        {
            if (!ModelState.IsValid)
            {
                string? contraseñaIngresada = Usuario.Contraseña?.Trim();
                string? correoIngresado = Usuario.Correo?.Trim();

                var usuario = _context.Usuarios
                    .FirstOrDefault(a =>a.Contraseña == contraseñaIngresada && a.Correo == correoIngresado);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Correo),  // Usar ClaimTypes.Name para identificar al usuario
                        new Claim(ClaimTypes.Email, usuario.Correo),
                        new Claim(ClaimTypes.Role, usuario.Rol),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Inicia sesión con el esquema correcto
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirecciona según el rol del usuario
                    switch (usuario.Rol)
                    {
                        case "Administrador":
                            return RedirectToAction("Menu", "DashboardAdmin"); 
                        case "Colaborador":
                            return RedirectToAction("Menu", "DashboardColaborador");
                        case "Cliente":
                            return RedirectToAction("Menu", "DashboardCliente");
                        default:
                            return RedirectToAction("Index", "Home"); // Fallback
                    }
                }

                ViewBag.Message = "Norreo o contraseña incorrectos.";
            }
            else
            {
                ViewBag.Message = "Error en el formulario. Por favor, verifique los campos.";
            }

            return View("~/Views/Usuarios/Login.cshtml");
        }

        // Cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
