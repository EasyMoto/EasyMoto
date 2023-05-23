namespace EasyMoto.Models
{
    /// <summary>
    /// Dados das categorias dos Produtos
    /// </summary>
    public class Categorias
    {
        public Categorias()
        {
            //Inicialização das Listas de Produtos e Utilizadores associadas às Categorias
            ListaProdutos = new HashSet<Produtos>();
            ListaUtilizadores = new HashSet<Utilizadores>();
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da Categoria
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Nome da Marca
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Lista dos Produtos de diferentes categorias
        /// </summary>
        public ICollection<Produtos> ListaProdutos { get; set; }

        /// <summary>
        /// Lista de Utilizadores (relação M-N)
        /// </summary>
        public ICollection<Utilizadores> ListaUtilizadores { get; set; }
    }
}
