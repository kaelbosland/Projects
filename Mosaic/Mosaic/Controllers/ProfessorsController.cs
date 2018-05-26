using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mosaic.Models;

namespace Mosaic.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly LoginSystemContext _context;

        public ProfessorsController(LoginSystemContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            var loginSystemContext = _context.Professor.Include(p => p.ClassOneNavigation);
            return View(await loginSystemContext.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.ClassOneNavigation)
                .SingleOrDefaultAsync(m => m.Username == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/CreateProf
        public IActionResult CreateProf()
        {
            ViewData["ClassOne"] = new SelectList(_context.Class, "ClassCode", "ClassCode");
            return View();
        }

        // POST: Professors/CreateProf
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProf([Bind("Username,Password,FirstName,LastName,ClassOne")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassOne"] = new SelectList(_context.Class, "ClassCode", "ClassCode", professor.ClassOne);
            return View(professor);
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.SingleOrDefaultAsync(m => m.Username == id);
            if (professor == null)
            {
                return NotFound();
            }
            ViewData["ClassOne"] = new SelectList(_context.Class, "ClassCode", "ClassCode", professor.ClassOne);
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,FirstName,LastName,ClassOne")] Professor professor)
        {
            if (id != professor.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.Username))
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
            ViewData["ClassOne"] = new SelectList(_context.Class, "ClassCode", "ClassCode", professor.ClassOne);
            return View(professor);
        }

        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.ClassOneNavigation)
                .SingleOrDefaultAsync(m => m.Username == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var professor = await _context.Professor.SingleOrDefaultAsync(m => m.Username == id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(string id)
        {
            return _context.Professor.Any(e => e.Username == id);
        }
    }
}
