using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Models
{
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
        public string Nome { get; set; }

        /// <summary>
        /// Preço do Produto
        /// </summary>
        public string Preco { get; set; }

        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Tamanho do Produto
        /// </summary>
        public string Tamanho { get; set; }

        /// <summary>
        /// Genero do Produto
        /// Masculino ou Feminino
        /// </summary>
        public string Genero { get; set; }

        /// <summary>
        /// Cor do Produto
        /// </summary>
        public string Cor { get; set; }

        /// <summary>
        /// Tipo de coleção do Produto (ex: Inverno, Verão, etc)
        /// </summary>
        public string Colecao { get; set; }

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
