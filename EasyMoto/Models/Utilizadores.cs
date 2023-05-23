using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Models
{
    /// <summary>
    /// Dados dos Utilizadores
    /// </summary>
    public class Utilizadores
    {
        public Utilizadores()
        {
            // Inicialização da Lista de Produtos associadas a um Utilizador
            ListaProdutos = new HashSet<Produtos>();
        }


        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Morada do utilizador
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal da morada do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Código Postal")]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-ZÇÁÉÍÓÚ]+[A-Z -ÁÉÍÓÚÇ]*",
           ErrorMessage = "O {0} deve ser escrito no formato XXXX-XXX NOME DA TERRA")]
        public string CodPostal { get; set; }

        /// <summary>
        /// Telemóvel do utilizador
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(9, MinimumLength = 9,
           ErrorMessage = "O {0} tem de ter {1} digitos")]
        [RegularExpression("9[1236][0-9]{7}",
           ErrorMessage = "Tem de escrever um nº de {0} válido")]
        public string Telemovel { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("[1235689][0-9]{8}", ErrorMessage ="O {0} deve ser válido")]
        [StringLength(9, MinimumLength =9, ErrorMessage = "O {0} deve ter {1} dígitos")]
        public string NIF { get; set; }

        // *********************************************

        /// <summary>
        /// Lista dos produtos propriedade do utilizador
        /// </summary>
        public ICollection<Produtos> ListaProdutos { get; set; }

        /// <summary>
        /// Lista dos produtos que o utilizador comprou
        /// (concretização do relacionamento M-N)
        /// </summary>
        public ICollection<Categorias> ListaCategorias { get; set; }

    }
}
