using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Data;
using EasyMoto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EasyMoto.Controllers
{

    [Authorize] // esta anotação obriga o utilizador a estar autenticado 

    public class UtilizadoresController : Controller
    {

        /// <summary>
        /// objeto para referenciar a base de dados 
        /// </summary>
        private readonly ApplicationDbContext _bd;

        /// <summary>
        /// ferramenta para aceder aos dados do utilizador
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        //construtor
        public UtilizadoresController(ApplicationDbContext context, UserManager<IdentityUser> userManager)

        {
            _bd = context;
            _userManager = userManager;

        }


        // GET: Utilizadores
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            if (User.Identity.Name == "admin@easymoto.com")
            {
                return _bd.Utilizadores != null ?
                          View(await _bd.Utilizadores.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Categorias'  is null.");
            }
            else
            {
                var userName = User.Identity.Name;
                var applicationDbContext = _bd.Utilizadores
                    .Where(a => a.Email == userName);
                return View(await applicationDbContext.ToListAsync());
            }
        }


        // GET: Utilizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _bd.Utilizadores == null)
            {
                return NotFound();
            }

            var utilizadores = await _bd.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);

            if (utilizadores == null)
            {
                return NotFound();
            }

            return View(utilizadores);
        }


        // GET: Utilizadores/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Nome,Morada,CodPostal,Telemovel,Email,NIF")] Utilizadores utilizadores)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _bd.Add(utilizadores);
        //        await _bd.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    // Apanhar mensagens de erro
        //    var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
        //                                          .Select(e => e.ErrorMessage)
        //                                          .ToList();


        //    // Passar mensagens de erro para a View 
        //    ViewBag.ErrorMessages = errorMessages;

        //    return View(utilizadores);
        //}

        // GET: Utilizadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _bd.Utilizadores == null)
            {
                return NotFound();
            }

            var utilizadores = await _bd.Utilizadores.FindAsync(id);
            if (utilizadores == null)
            {
                return NotFound();
            }
            return View(utilizadores);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Morada,CodPostal,Telemovel,Email,NIF")] Utilizadores utilizadores)
        {
            if (id != utilizadores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bd.Update(utilizadores);
                    await _bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadoresExists(utilizadores.Id))
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
            return View(utilizadores);
        }

        // GET: Utilizadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _bd.Utilizadores == null)
            {
                return NotFound();
            }

            var utilizadores = await _bd.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            return View(utilizadores);
        }

        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_bd.Utilizadores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Utilizadores'  is null.");
            }
            var utilizadores = await _bd.Utilizadores.FindAsync(id);
            if (utilizadores != null)
            {
                _bd.Utilizadores.Remove(utilizadores);
            }

            await _bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadoresExists(int id)
        {
            return (_bd.Utilizadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}