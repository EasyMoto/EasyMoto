using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Models
{
    /// <summary>
    /// Dados dos Utilizadores
    /// </summary>
    public class Utilizadores
    {
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Morada do utilizador
        /// </summary>
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal da morada do utilizador
        /// </summary>
        public string CodigoPostal { get; set; }

        /// <summary>
        /// Telemóvel do utilizador
        /// </summary>
        public string Telemovel { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// NIF do utilizador
        /// </summary>
        public string NIF { get; set; }


    }
}
