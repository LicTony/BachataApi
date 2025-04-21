using System.ComponentModel.DataAnnotations;

namespace BachataApi.Configuration
{
    public class JwtSettings
    {
        [Required(ErrorMessage = "Parametro Key es requerido")]
        public required string Key { get; set; }

        [Required(ErrorMessage = "Parametro Issuer es requerido")]
        public required string Issuer { get; set; }

        [Required(ErrorMessage = "Parametro Audience es requerido")]
        public required string Audience { get; set; }


        [Required(ErrorMessage = "Parametro ExpireMinutes es requerido")]
        [Range(1, 1440)]
        public required int ExpireMinutes { get; set; }


    }
}
