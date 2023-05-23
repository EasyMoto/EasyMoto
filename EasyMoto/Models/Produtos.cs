using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Models
{
    public enum TamanhoProduto
    {
        XXS,
        XS,
        S,
        M,
        L,
        XL,
        XXL,
        XXXL
    }

    public enum TiposGenero
    {
        Masculino,
        Feminino,
        Unissexo
    }

    public enum TiposColecao
    {
        Verão,
        Inverno,
        [Display(Name = "Todo o Ano")]
        TodoOAno
    }



    /// <summary>
    /// Dados dos Produtos
    /// </summary>
    public class Produtos
    {
        public Produtos()
        {
            /// Inicialização para a Lista de Fotografias associadas a cada Produto
            ListaFotografias = new HashSet<Fotografias>();
        }
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome do {0} deve ter de {2} a {1} caracteres")]
        [RegularExpression("[A-Za-zÀÈÌÒÙÁÉÍÓÚÃÕÇÀÈÌÒÙáéíóúãõ ç0-9]+", ErrorMessage = "Deve introduzir um {0} válido")]
        public string Nome { get; set; }

        /// <summary>
        /// Preço do Produto
        /// </summary>
        /// 
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "O {0} deve ter de {2} a {1} caracteres")]
        [RegularExpression("[0-9,€]+", ErrorMessage = "Deve introduzir um {0} válido")]
        [Display(Name = "Preço")]
        public string Preco { get; set; }

        /// <summary>
        /// Descrição do Produto
        /// </summary>
        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O {0} deve ter de {2} a {1} caracteres")]
        public string Descricao { get; set; }

        /// <summary>
        /// Tamanho do Produto
        /// XXS, XS, S, M, L, XL, XXL, XXXL
        /// </summary>
        public TamanhoProduto Tamanho { get; set; }

        /// <summary>
        /// Genero do Produto
        /// Masculino, Feminino ou Unissexo
        /// </summary>
        public TiposGenero Genero { get; set; }

        /// <summary>
        /// Cor do Produto
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "A {0} deve ter de {2} a {1} caracteres")]
        [RegularExpression("[A-Za-zÀÈÌÒÙÁÉÍÓÚÃÕÇÀÈÌÒÙáéíóúãõ ç]+")]
        public string Cor { get; set; }

        /// <summary>
        /// Tipo de coleção do Produto
        /// Inverno, Verão, Todo o Ano )
        /// </summary>
        [Display(Name =" Coleção")]
        public TiposColecao Colecao { get; set; }

        ///
        /// Chaves Forasteiras
        /// 

        /// <summary>
        /// Lista das Fotografias associadas a cada Produto
        /// </summary>
        public ICollection<Fotografias> ListaFotografias { get; set; }

        /// <summary>
        /// FK para a Categoria do Produto
        /// </summary>
        [ForeignKey(nameof(Categoria))]
        public int CategoriaFK { get; set; }
        public Categorias Categoria { get; set; }

        /// <summary>
        /// FK para o Utilizador
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizadores Utilizador { get; set; }
    }
}
