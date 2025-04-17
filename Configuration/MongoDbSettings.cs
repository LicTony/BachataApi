using System.ComponentModel.DataAnnotations;

namespace BachataApi.Configuration
{
    public class MongoDbSettings
    {
        [Required(ErrorMessage = "Parametro ConnectionString es requerido")]
        public required string ConnectionString { get; set; }

        [Required(ErrorMessage = "Parametro DatabaseName es requerido")]
        public required string DatabaseName { get; set; }

        [Required(ErrorMessage = "Parametro FigurasCollectionName es requerido")]
        public required string FigurasCollectionName { get; set; }
    }
}
