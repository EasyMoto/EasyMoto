namespace EasyMoto.Models
{
    /// <summary>
    /// Dados dos Produtos
    /// </summary>
    public class Produtos
    {
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
    }
}
