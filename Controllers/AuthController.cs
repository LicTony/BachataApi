using BachataApi.Configuration;
using BachataApi.DTOs;
using BachataApi.Models;
using BachataApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;

namespace BachataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserService _userService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(JwtService jwtService, UserService userService , IOptions<JwtSettings> jwtOptions)
        {
            _jwtService = jwtService;
            _userService = userService;
            _jwtSettings = jwtOptions.Value;
        }



        /// <summary>
        /// Valida la clave del usuario y retorna un token JWT
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            if(!_userService.ValidarUsuario(request , out Usuario usuario))
                return Unauthorized("Credenciales inválidas");

            var token = _jwtService.GenerateToken(usuario.Id.ToString(), usuario.Email);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes) // o usar desde JwtSettings si preferís
            });
            
            
        }
    }
}
