using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(15, MinimumLength = 3, ErrorMessage ="O nome da {0} deve ter de {2} a {1} caracteres")]
        [RegularExpression("[A-Za-zÀÈÌÒÙÁÉÍÓÚÃÕÇÀÈÌÒÙáéíóúãõ ç]+", ErrorMessage = "Deve introduzir um {0} válido")]
        public string Nome { get; set; }

        /// <summary>
        /// Nome da Marca
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "A {0} deve ter de {2} a {1} caracteres")]
        [RegularExpression("[A-Za-zÀÈÌÒÙÁÉÍÓÚÃÕÇÀÈÌÒÙáéíóúãõ ç]+", ErrorMessage = "Deve introduzir um {0} válido")]
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
