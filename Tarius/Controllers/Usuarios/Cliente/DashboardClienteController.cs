using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tarius.Controllers.Usuarios.Cliente
{
    [Authorize(Roles = "Cliente")]
    public class DashboardClienteController : Controller
    {
        public IActionResult Menu()
        {
            return View("~/Views/Usuarios/Cliente/Menu.cshtml"); 
        }

        public IActionResult MisIngredientes()
        {
            return RedirectToAction("Ingredientes", "MisIngredientes");
        }

        public IActionResult ListaCompras()
        {
            return RedirectToAction("ListaCompras", "RecetasCliente");
        }

        [HttpPost]
        public IActionResult Recetas()
        {
            return RedirectToAction("VerRecetas", "RecetasCliente");
        }

        public IActionResult Planes()
        {
            return RedirectToAction("VerPlanes", "PlanesCliente", new { area = "" });
        }

    }
}
