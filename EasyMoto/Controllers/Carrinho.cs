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
    //Os outros métodos foram apagados porque não vamos precisar
    }
}
