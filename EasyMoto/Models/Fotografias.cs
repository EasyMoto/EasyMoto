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
        public string Ficheiro { get; set; }

        /// <summary>
        /// Data da Fotografia
        /// </summary>
        public DateTime Data { get; set; }
    }
}
