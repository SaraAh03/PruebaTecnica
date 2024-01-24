using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class ProyectoesController : Controller
    {
        private readonly AsoConstructorasContext _context;

        public ProyectoesController(AsoConstructorasContext context)
        {
            _context = context;
        }

        // GET: Proyectoes
        public async Task<IActionResult> Index(string searchTerm)
        {
            //var proyectos = await _context.Proyectos.Include(p => p.PersonasInteresada).ToListAsync();
            // return View(proyectos);
            var proyectosQuery = _context.Proyectos.Include(p => p.PersonasInteresada).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Aplica el filtro de búsqueda
                proyectosQuery = proyectosQuery.Where(p =>
                    p.Codigo.Contains(searchTerm) ||
                    p.Nombre.Contains(searchTerm) ||
                    p.Direccion.Contains(searchTerm) ||
                    p.Constructora.Contains(searchTerm) ||
                    p.Contacto.Contains(searchTerm)
                );
            }

            var proyectos = await proyectosQuery.ToListAsync();
            return View(proyectos);
        }

        // GET: Proyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
            .Include(p => p.PersonasInteresada)  // Asegúrate de incluir la relación
            .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewBag.CantidadPersonas = proyecto.PersonasInteresada.Count;

            return View(proyecto);
        }

        // GET: Proyectoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Nombre,Direccion,Constructora,Contacto")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            return View(proyecto);
        }

        // POST: Proyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Nombre,Direccion,Constructora,Contacto")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        // GET: Proyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        public IActionResult AgregarPersonaInteresada(int proyectoId)
        {
            ViewBag.ProyectoId = proyectoId;
            ViewBag.CodigoProyecto = _context.Proyectos.FirstOrDefault(p => p.Id == proyectoId)?.Codigo;
            return View("~/Views/PersonasInteresadas/Create.cshtml");
        }


        // POST: Proyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var proyecto = await _context.Proyectos.FindAsync(id);
                if (proyecto != null)
                {
                    _context.Proyectos.Remove(proyecto);
                    await _context.SaveChangesAsync();

                }

            return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                TempData["ErrorMessage"] = "No se puede eliminar el proyecto porque tiene personas asociadas.";
                return RedirectToAction(nameof(Index));
            }

        }


        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
