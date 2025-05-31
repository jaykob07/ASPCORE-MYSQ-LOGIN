using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportesMVC.Data;
using ReportesMVC.Models;

namespace ReportesMVC.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var personas = await _context.Personas.ToListAsync();
            return View(personas);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();

            return View(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Personas.Update(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // delete use post on razor  
        [HttpPost] 
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return NotFound();

            try
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // log error
                ModelState.AddModelError("", "No se pudo eliminar la persona.");
                return RedirectToAction("Index"); // o muestra error
            }
        }


        [HttpGet]
        public async Task<IActionResult> BuscarPersonas(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return Json(new List<object>());

            var personas = await _context.Personas
                .Where(p => p.Nombre.Contains(termino) || 
                        p.ApPaterno.Contains(termino) || 
                        p.ApMaterno.Contains(termino) ||
                        p.NumeroIdentificacion.Contains(termino))
                .Select(p => new {
                    id = p.Id,
                    nombre = p.Nombre,
                    apPaterno = p.ApPaterno,
                    apMaterno = p.ApMaterno,
                    correo = p.Correo,
                    numeroIdentificacion = p.NumeroIdentificacion
                })
                .Take(10) // Limitar resultados
                .ToListAsync();

            return Json(personas);
        }

    }
}
