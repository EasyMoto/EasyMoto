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
    [Authorize]
    public class Carrinho : Controller
    {
        private readonly ApplicationDbContext _bd;
        /// <summary>
        /// aceder aos dados do utilizador
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public Carrinho(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _bd = context;
            _userManager = userManager;
        }

        // GET: Carrinho
        public async Task<IActionResult> Index()
        {
            //não esquecer de juntar a lista de fotografias se não as mesmas não aparecem no modelo dos Utilizadores
              return View(await _bd.Utilizadores.Include(a => a.ListaCategorias)
                                                .Include(a => a.ListaProdutos)
                                                .ThenInclude(ap => ap.ListaFotografias)
                                                .ToListAsync());
        }

        // GET: Carrinho/Details/5
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

        // GET: Carrinho/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carrinho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Morada,CodPostal,Telemovel,Email,UserId,NIF")] Utilizadores utilizadores)
        {
            if (ModelState.IsValid)
            {
                _bd.Add(utilizadores);
                await _bd.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilizadores);
        }

        // GET: Carrinho/Edit/5
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

        // POST: Carrinho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Morada,CodPostal,Telemovel,Email,UserId,NIF")] Utilizadores utilizadores)
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

        // GET: Carrinho/Delete/5
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

        // POST: Carrinho/Delete/5
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
          return _bd.Utilizadores.Any(e => e.Id == id);
        }
    }
}
