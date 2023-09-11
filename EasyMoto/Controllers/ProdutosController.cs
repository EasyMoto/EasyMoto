﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Data;
using EasyMoto.Models;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EasyMoto.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        /// <summary>
        /// atributo para representar a Base de Dados
        /// </summary>
        private readonly ApplicationDbContext _bd;

        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// aceder aos dados do utilizador
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        //Instanciar no Construtor
        public ProdutosController(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<IdentityUser> userManager)
        {

            _bd = context;
            _environment = environment;
            _userManager = userManager;
        }

        /// <summary>
        /// Devolve um objecto do tipo "ActionResult" - invoca uma view, prepara um trabalho e entrega à view
        /// </summary>
        /// <returns>
        /// Return  await porque o método é assíncrono
        /// </returns>
        // GET: Produtos
        [AllowAnonymous]
        public async Task<IActionResult> Index() //invoka a view que está aqui (Index)
        {
            var applicationDbContext = _bd.Produtos
                                                    .Include(p => p.Categoria)
                                                    .Include(p => p.Utilizador)
                                                    .Include(p => p.ListaFotografias)
                                                    .Where(a => a.Utilizador.Nome == "Administrador");


            // invoco a view, fornecendo-lhe os dados que ela necessita
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Produtos/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)//"?" significa que é um parâmetro que pode ser nulo
        {// proteção à pesquisa, caso a tabela dos Produtos esteja vazia ou o Id seja nulo
            if (id == null || _bd.Produtos == null)
            {
                //"?" significa que é um parâmetro que pode ser nulo
                return NotFound();
            }

            // pesquisar os dados dos produtos, para os mostrar no ecrã
            var produto = await _bd.Produtos// alterar nome da variavel para singular porque apenas estamos à procura de um produto
                .Include(p => p.Categoria)
                .Include(p => p.Utilizador)
                .Include(p => p.ListaFotografias)
                .FirstOrDefaultAsync(m => m.Id == id);// igual ao LIMIT 1 caso retorne mais do que 1 resultado

            // proteção à pesquisa, caso o Id não exista
            if (produto == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }


        /// <summary>
        /// Método para adicionar o produto ao carrinho do utilizador autenticado
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AdicionarCarrinho(int produtoId, string userEmail)
        {
            // Dados 
            var produto = await _bd.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Utilizador)
                .Include(p => p.ListaFotografias)
                .FirstOrDefaultAsync(m => m.Id == produtoId);

            var user = await _bd.Utilizadores.FirstOrDefaultAsync(u => u.Email == userEmail);


            if (produto != null && user != null)
            {
                // Associar o produto com o utilizador 
                produto.UtilizadorFK = user.Id;

                // guardar alterações na BD
                await _bd.SaveChangesAsync();

                // Redirecionamento a avisar que foi adicionado o produto ao carrinho
                return RedirectToAction(nameof(Index), "Carrinho");
            }

            // Erro a adicionar ao carrinho
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// invoca a view para criar um novo produto
        /// </summary>
        /// <returns></returns>
        // GET: Produtos/Create
        public IActionResult Create()
        {
            if (User.Identity.Name != "admin@easymoto.com")
            {
                //retornar para Index se o utilizador não tiver acesso
                return RedirectToAction("Index");
            }
            //chaves forasteiras para as tabelas "Categorias" e " Utilizadores"
            //preparar os dados que vão ficar associados às chaves forasteiras - trasportar dados do Controller para a View
            ViewBag.CategoriaNome = new SelectList(_bd.Categorias, "Id", "Nome");
            ViewBag.CategoriaMarca = new SelectList(_bd.Categorias, "Id", "Marca");
            ViewData["UtilizadorFK"] = new SelectList(_bd.Utilizadores, "Id", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// o metodo create é usado quando fazemos o submit do novo produto e vamos ter um atributo = produto (SINGULAR) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,Descricao,Tamanho,Genero,Cor,Colecao,CategoriaFK,MarcaFK,UtilizadorFK")] Produtos produto, IFormFile fotografia)
        {
            //vars auxiliar
            bool existeFoto = false;
            string nomeFoto = "";

            if (produto.CategoriaFK == 0)
            {
                // se escolhi uma categoria
                // gerar mensagem de erro
                ModelState.AddModelError("", "Deve escolher uma categoria, por favor.");
            }
            else
            {
                if (produto.UtilizadorFK == 0)
                // não escolhi o utilizador
                {
                    ModelState.AddModelError("", "Deve escolher um utilizador, por favor.");
                }
                else
                {
                    // se cheguei aqui é porqu e escolhi uma categoria e um utilizador
                    // vamos avaliar o ficheiro, se é que ele existe 
                    if (fotografia == null)
                    {
                        // não há ficheiro (imagem)
                        produto.ListaFotografias.Add(new Fotografias
                        {
                            Data = DateTime.Now,
                            Ficheiro = "noProduct.jpg"
                        });

                    }
                    else
                    {

                        // se chego aqui, existe ficheiro
                        // mas será imagem?
                        if (fotografia.ContentType != "image/jpeg" &&
                            fotografia.ContentType != "image/png")
                        {
                            // existe ficheiro mas não é uma imagem 
                            // <=> ! (fotografia.ContentType == "image/jpeg" || fotografia.ContentType == "image/png")

                            ModelState.AddModelError("", "Forneceu um ficheiro que não é uma imagem. Escolha, um ficheiro do tipo PNG ou JPG.");
                        }

                        else
                        {
                            // há imagem 
                            existeFoto = true;
                            // definir o nome do ficheiro
                            Guid g = Guid.NewGuid();
                            // transforma o guid que é um objeto no texto correspondente 
                            nomeFoto = produto.UtilizadorFK + "_" + g.ToString();
                            // o objeto path dá-nos a extensão do nome que é colado no fileName, tolower para ficar tudo em minusculas(?)
                            string extensao = Path.GetExtension(fotografia.FileName).ToLower();
                            nomeFoto += extensao;
                            // onde o guardar?
                            // vamos gurardor o ficheiro na pasta 'wwwroot'
                            // mas apenas após guardarmos os dados do produto na BD 

                            // guardar os dados do ficheiro na BD
                            produto.ListaFotografias
                                .Add(new Fotografias
                                {
                                    Ficheiro = nomeFoto,
                                    Data = DateTime.Now,
                                });


                        }
                    }
                }
            }



            try
            {
                if (ModelState.IsValid)
                {
                    // adicionar os dados do 'produto'
                    // à BD, mas apenas na memoria do servidor web
                    _bd.Add(produto);
                    // transferir os dados para a BD
                    await _bd.SaveChangesAsync();

                    // se cheguei aqui, vamos guardar o ficheiro no disco rígido
                    if (existeFoto)
                    {
                        string nomePastaFoto = _environment.WebRootPath;
                        // juntar o nome da pasta onde serão guardadas as imagens
                        nomePastaFoto = Path.Combine(nomePastaFoto, "imagens");
                        //mas a pasta existe?
                        if (!Directory.Exists(nomePastaFoto))
                        {
                            Directory.CreateDirectory(nomePastaFoto);
                        }

                        //vamos iniciar a escrita do ficheiro no disco rigido
                        nomeFoto = Path.Combine(nomePastaFoto, nomeFoto);
                        using var stream = new FileStream(nomeFoto, FileMode.Create);
                        await fotografia.CopyToAsync(stream);


                    }


                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)

            {
                ModelState.AddModelError("", "Ocorreu um erro no acesso à base de dados");
                //throw;
            }

            // preparar os dados para serem devolvidos para  View 

            ViewBag.CategoriaNome = new SelectList(_bd.Categorias.OrderBy(r => r.Nome), "Id", "Nome", produto.CategoriaFK);
            ViewBag.CategoriaMarca = new SelectList(_bd.Categorias, "Id", "Marca", produto.CategoriaFK);
            ViewData["UtilizadorFK"] = new SelectList(_bd.Utilizadores, "Id", "Nome", produto.UtilizadorFK);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.Name != "admin@easymoto.com")
            {
                //retornar para Index se o utilizador não tiver acesso
                return RedirectToAction("Details", new { id = id });
            }
            if (id == null || _bd.Produtos == null)
            {
                return RedirectToAction("Index");
            }

            //para que não seja possível alterar o URL e editar produtos
            var produtos = await _bd.Produtos
                                     .Include(p => p.ListaFotografias)
                                     .Where(a => a.Id == id &&
                                                 a.Utilizador.Nome == "Administrador")
                                     .FirstOrDefaultAsync();

            if (produtos == null)
            {   //redirecionamento para a pagina do Index
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriaNome = new SelectList(_bd.Categorias, "Id", "Nome", produtos.CategoriaFK);
            ViewBag.CategoriaMarca = new SelectList(_bd.Categorias, "Id", "Marca", produtos.CategoriaFK);
            ViewData["UtilizadorFK"] = new SelectList(_bd.Utilizadores, "Id", "Nome", produtos.UtilizadorFK);
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Descricao,Tamanho,Genero,Cor,Colecao,CategoriaFK,MarcaFK,UtilizadorFK,SelectedPhotos")] Produtos produto, IFormFile fotografia, List<string> selectedPhotos)
        {
            if (id != produto.Id)
            {
                return RedirectToAction("Index");
            }

            //vars auxiliar
            bool existeFoto = false;
            string nomeFoto = "";

            if (produto.CategoriaFK == 0)
            {
                // se escolhi uma categoria
                // gerar mensagem de erro
                ModelState.AddModelError("", "Deve escolher uma categoria, por favor.");
            }
            else
            {
                if (produto.UtilizadorFK == 0)
                // não escolhi o utilizador
                {
                    ModelState.AddModelError("", "Deve escolher um utilizador, por favor.");
                }
                else
                {
                    var countFotos = produto.ListaFotografias.Count;
                    // se cheguei aqui é porqu e escolhi uma categoria e um utilizador
                    // vamos avaliar o ficheiro, se é que ele existe 
                    if (fotografia == null && countFotos != 0)
                    {
                        // não há ficheiro (imagem)
                        produto.ListaFotografias.Add(new Fotografias
                        {
                            Data = DateTime.Now,
                            Ficheiro = "noProduct.jpg"
                        });

                    }
                    else if (fotografia != null)
                    {

                        // se chego aqui, existe ficheiro
                        // mas será imagem?
                        if (fotografia.ContentType != "image/jpeg" &&
                            fotografia.ContentType != "image/png")
                        {
                            // existe ficheiro mas não é uma imagem 
                            // <=> ! (fotografia.ContentType == "image/jpeg" || fotografia.ContentType == "image/png")

                            ModelState.AddModelError("", "Forneceu um ficheiro que não é uma imagem. Escolha, um ficheiro do tipo PNG ou JPG.");
                        }

                        else
                        {
                            // há imagem 
                            existeFoto = true;
                            // definir o nome do ficheiro
                            Guid g = Guid.NewGuid();
                            // transforma o guid que é um objeto no texto correspondente 
                            nomeFoto = produto.UtilizadorFK + "_" + g.ToString();
                            // o objeto path dá-nos a extensão do nome que é colado no fileName, tolower para ficar tudo em minusculas(?)
                            string extensao = Path.GetExtension(fotografia.FileName).ToLower();
                            nomeFoto += extensao;
                            // onde o guardar?
                            // vamos gurardor o ficheiro na pasta 'wwwroot'
                            // mas apenas após guardarmos os dados do produto na BD 

                            // guardar os dados do ficheiro na BD
                            produto.ListaFotografias
                                .Add(new Fotografias
                                {
                                    Ficheiro = nomeFoto,
                                    Data = DateTime.Now,
                                });
                        }
                    }
                }
            }

            foreach (var file in selectedPhotos)
            {
                //Encontrar a foto na lista de fotografias
                var photo = produto.ListaFotografias.FirstOrDefault(f => f.Ficheiro == file);
                var photoToDelete = _bd.Fotografias.FirstOrDefault(f => f.Ficheiro == file);
                if ( photoToDelete != null)
                {
                    // Remover a fotografia da lista
                    //produto.ListaFotografias.Remove(photo);
                    _bd.Fotografias.Remove(photoToDelete);
                }
                await _bd.SaveChangesAsync();
                // Delete the photo file from the wwwroot/images folder
                //var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", filename);
                //if (File.Exists(imagePath))
                //{
                //    File.Delete(imagePath);
                //}
            }


            try
            {
                if (ModelState.IsValid)
                {
                    // adicionar os dados do 'produto'
                    // à BD, mas apenas na memoria do servidor web
                    _bd.Update(produto);
                    // transferir os dados para a BD
                    await _bd.SaveChangesAsync();

                    // se cheguei aqui, vamos guardar o ficheiro no disco rígido
                    if (existeFoto)
                    {
                        string nomePastaFoto = _environment.WebRootPath;
                        // juntar o nome da pasta onde serão guardadas as imagens
                        nomePastaFoto = Path.Combine(nomePastaFoto, "imagens");
                        //mas a pasta existe?
                        if (!Directory.Exists(nomePastaFoto))
                        {
                            Directory.CreateDirectory(nomePastaFoto);
                        }

                        //vamos iniciar a escrita do ficheiro no disco rigido
                        nomeFoto = Path.Combine(nomePastaFoto, nomeFoto);
                        using var stream = new FileStream(nomeFoto, FileMode.Create);
                        await fotografia.CopyToAsync(stream);


                    }


                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
  
                ModelState.AddModelError("", "Ocorreu um erro no acesso à base de dados");
                //throw;
            }


            ViewBag.CategoriaNome = new SelectList(_bd.Categorias, "Id", "Nome", produto.CategoriaFK);
            ViewBag.CategoriaMarca = new SelectList(_bd.Categorias, "Id", "Marca", produto.CategoriaFK);
            ViewData["UtilizadorFK"] = new SelectList(_bd.Utilizadores, "Id", "Nome", produto.UtilizadorFK);
            return View(produto);
        }


        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.Name != "admin@easymoto.com")
            {
                //retornar para Index se o utilizador não tiver acesso
                return RedirectToAction("Index");
            }
            if (id == null || _bd.Produtos == null)
            {
                return RedirectToAction("Index");
            }

            var produtos = await _bd.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtos == null)
            {
                return RedirectToAction("Index");
            }

            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_bd.Produtos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produtos'  is null.");
            }
            var produtos = await _bd.Produtos.FindAsync(id);
            if (produtos != null)
            {
                _bd.Produtos.Remove(produtos);
            }

            await _bd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutosExists(int id)
        {
            return _bd.Produtos.Any(e => e.Id == id);
        }

    }
}