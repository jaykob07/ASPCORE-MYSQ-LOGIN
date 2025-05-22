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
    }
}
