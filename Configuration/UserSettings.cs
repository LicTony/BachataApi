using System.ComponentModel.DataAnnotations;

namespace BachataApi.Configuration
{
    public class UserSettings
    {

        [Required(ErrorMessage = "Parametro Id es requerido")]
        public required int Id { get; set; }


        [Required(ErrorMessage = "Parametro Email es requerido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Parametro Password es requerido")]
        public required string Password { get; set; }



    }
}
