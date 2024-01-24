using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class PersonasInteresadasController : Controller
    {
        private readonly AsoConstructorasContext _context;

        public PersonasInteresadasController(AsoConstructorasContext context)
        {
            _context = context;
        }

        // GET: PersonasInteresadas
        public async Task<IActionResult> Index()
        {
            var asoConstructorasContext = _context.PersonasInteresadas.Include(p => p.Proyecto);
            return View(await asoConstructorasContext.ToListAsync());
        }

        // GET: PersonasInteresadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personasInteresada = await _context.PersonasInteresadas
                .Include(p => p.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personasInteresada == null)
            {
                return NotFound();
            }

            return View(personasInteresada);
        }

        // GET: PersonasInteresadas/Create
        public IActionResult Create()
        {
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id");
            return View();
        }

        // POST: PersonasInteresadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,FechaNacimiento,ProyectoId")] PersonasInteresada personasInteresada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personasInteresada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", personasInteresada.ProyectoId);
            return View(personasInteresada);
        }

        // GET: PersonasInteresadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personasInteresada = await _context.PersonasInteresadas.FindAsync(id);
            if (personasInteresada == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", personasInteresada.ProyectoId);
            return View(personasInteresada);
        }

        // POST: PersonasInteresadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Correo,FechaNacimiento,ProyectoId")] PersonasInteresada personasInteresada)
        {
            if (id != personasInteresada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personasInteresada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonasInteresadaExists(personasInteresada.Id))
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
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", personasInteresada.ProyectoId);
            return View(personasInteresada);
        }

        // GET: PersonasInteresadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personasInteresada = await _context.PersonasInteresadas
                .Include(p => p.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personasInteresada == null)
            {
                return NotFound();
            }

            return View(personasInteresada);
        }

        // POST: PersonasInteresadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personasInteresada = await _context.PersonasInteresadas.FindAsync(id);
            if (personasInteresada != null)
            {
                _context.PersonasInteresadas.Remove(personasInteresada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonasInteresadaExists(int id)
        {
            return _context.PersonasInteresadas.Any(e => e.Id == id);
        }
    }
}
