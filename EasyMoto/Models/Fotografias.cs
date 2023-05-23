using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Models
{
    /// <summary>
    /// Dados das Fotografias
    /// </summary>
    public class Fotografias
    {

        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do ficheiro da Fotografia
        /// </summary>
        [Required(ErrorMessage= "O nome do {0} é de preenchimento obrigatório")]
        public string Ficheiro { get; set; }

        /// <summary>
        /// Data da Fotografia
        /// </summary>
        /// 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        ///
        /// Chaves Forasteiras
        /// 

        /// <summary>
        /// FK para o Produto a que Fotografia está associada
        /// </summary>
        [ForeignKey(nameof(Produto))]
        public int ProdutoFK { get; set; }
        public Produtos Produto { get; set; }
    }
}
