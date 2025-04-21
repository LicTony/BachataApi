using BachataApi.Configuration;
using BachataApi.DTOs;
using BachataApi.Models;
using Microsoft.Extensions.Options;

namespace BachataApi.Services
{
    public class UserService
    {

        private readonly UserSettings _settings;

        public UserService(IOptions<UserSettings> options)
        {
            _settings = options.Value;
        }

        public bool ValidarUsuario(LoginRequestDto loginRequest , out Usuario usuario) 
        {
            usuario = new Usuario();


            // Usuario simulado (en el futuro: validar desde base de datos)
            if (loginRequest.Email == _settings.Email && loginRequest.Password == _settings.Password)
            {
                usuario.Id = _settings.Id;
                usuario.Email = _settings.Email;
                usuario.Password = "";
                return true;
            }


            return false;




        }

    }
}
