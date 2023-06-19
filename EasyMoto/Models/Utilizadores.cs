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
        [StringLength(40, MinimumLength = 6, ErrorMessage ="O {0} deve ter no mínimo {2} e no máximo {1} caracteres")]
        [RegularExpression("[A-Za-zÀÈÌÒÙÁÉÍÓÚÃÕÇÀÈÌÒÙáéíóúãõ ç]+", ErrorMessage = "Deve introduzir um {0} válido")]
        public string Nome { get; set; }

        /// <summary>
        /// Morada do utilizador
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "A {0} deve ter no mínimo {1} caracteres")]
        [RegularExpression("[A-Za-z0-9ÀÈÌÒÙÁÉÍÓÚÃÕÇÀÈÌÒÙáéíóúãõç, -]+", ErrorMessage = "Deve introduzir uma {0} válida")]
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal da morada do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Código Postal")]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-ZÇÁÉÍÓÚÃÀÈÌÒÙ]+[A-Z -ÁÃÉÍÓÚÇÀÈÌÒÙ]*", ErrorMessage = "O {0} deve ser escrito no formato XXXX-XXX NOME DA TERRA")]
        public string CodPostal { get; set; }

        /// <summary>
        /// Telemóvel do utilizador
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} tem de ter {1} digitos")]
        [RegularExpression("9[1236][0-9]{7}", ErrorMessage = "Tem de escrever um nº de {0} válido")]
        public string Telemovel { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "Deve introduzir um {0} válido")]
        public string Email { get; set; }


        //********************************************


        /// <summary>
        ///quando o utilizador colocar um email vou ter uma chave FORASTEIRA que vai fazer a sincronização
        ///elemento de ligação entre a base de dados da autenticação e a base de dados do 'negócio'
        /// </summary>
        public string UserId { get; set; }

        //********************************************



        /// <summary>
        /// NIF do Utilizador
        /// </summary>
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} deve ter {1} dígitos")]
        [RegularExpression("[1235689][0-9]{8}", ErrorMessage = "Tem de escrever um {0} válido")]
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
