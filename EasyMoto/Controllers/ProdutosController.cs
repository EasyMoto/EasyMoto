using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Data;
using EasyMoto.Models;

namespace EasyMoto.Controllers
{
    public class ProdutosController : Controller
    {   
        /// <summary>
        /// atribuot para representar a Base de Dados
        /// </summary>
        private readonly ApplicationDbContext _context;
        //Instanciar no Construtor
        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve um objecto do tipo "ActionResult" - invoca uma view, prepara um trabalho e entrega à view
        /// </summary>
        /// <returns>
        /// Return  await porque o método é assíncrono
        /// </returns>
        // GET: Produtos
        public async Task<IActionResult> Index() //invoka a view que está aqui (Index)
        {
            var applicationDbContext = _context.Produtos
                                                    .Include(p => p.Categoria)
                                                    .Include(p => p.Utilizador);


            // invoco a view, fornecendo-lhe os dados que ela necessita
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)//"?" significa que é um parâmetro que pode ser nulo
        {// proteção à pesquisa, caso a tabela dos Produtos esteja vazia ou o Id seja nulo
            if (id == null || _context.Produtos == null)
            {
                //"?" significa que é um parâmetro que pode ser nulo
                return NotFound();
            }

            // pesquisar os dados dos animais, para os mostrar no ecrã
            var produto = await _context.Produtos// alterar nome da variavel para singular porque apenas estamos à procura de um animal
                .Include(p => p.Categoria)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);// igual ao LIMIT 1 caso retorne mais do que 1 resultado

            // proteção à pesquisa, caso o Id não exista
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        /// <summary>
        /// invoca a view para criar um novo produto
        /// </summary>
        /// <returns></returns>
        // GET: Produtos/Create
        public IActionResult Create()
        {
            //chaves forasteiras para as tabelas "Categorias" e " Utilizadores"
            //preparar os dados que vão ficar associados às chaves forasteiras - trasportar dados do Controller para a View
            ViewBag.CategoriaNome = new SelectList(_context.Categorias, "Id", "Nome");
            ViewBag.CategoriaMarca = new SelectList(_context.Categorias, "Id", "Marca");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,Descricao,Tamanho,Genero,Cor,Colecao,CategoriaFK,MarcaFK,UtilizadorFK")] Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriaNome = new SelectList(_context.Categorias, "Id", "Nome", produtos.CategoriaFK);
            ViewBag.CategoriaMarca = new SelectList(_context.Categorias, "Id", "Marca", produtos.CategoriaFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", produtos.UtilizadorFK);
            return View(produtos);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }
            ViewBag.CategoriaNome = new SelectList(_context.Categorias, "Id", "Nome", produtos.CategoriaFK);
            ViewBag.CategoriaMarca = new SelectList(_context.Categorias, "Id", "Marca", produtos.CategoriaFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", produtos.UtilizadorFK);
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Descricao,Tamanho,Genero,Cor,Colecao,CategoriaFK,UtilizadorFK")] Produtos produtos)
        {
            if (id != produtos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutosExists(produtos.Id))
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
            ViewBag.CategoriaNome = new SelectList(_context.Categorias, "Id", "Nome", produtos.CategoriaFK);
            ViewBag.CategoriaMarca = new SelectList(_context.Categorias, "Id", "Marca", produtos.CategoriaFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", produtos.UtilizadorFK);
            return View(produtos);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produtos'  is null.");
            }
            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos != null)
            {
                _context.Produtos.Remove(produtos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutosExists(int id)
        {
          return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
