using Microsoft.AspNetCore.Mvc;
using ReportesMVC.Data;
using ReportesMVC.Models;
using System.Linq;

namespace ReportesMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string correo, string contrasena)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == correo && u.Contrasena == contrasena);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Persona");
            }

            ViewBag.Mensaje = "Usuario o contraseña incorrectos.";
            return View();
        }
    }
}
