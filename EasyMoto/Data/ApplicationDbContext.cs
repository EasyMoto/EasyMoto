using EasyMoto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // ********************************************
        // Criação das tabelas da BD
        // ********************************************
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Utilizadores> Utilizadores{ get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Fotografias> Fotografias { get; set; }


    }
}