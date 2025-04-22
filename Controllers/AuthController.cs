using BachataApi.Configuration;
using BachataApi.DTOs;
using BachataApi.Models;
using BachataApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BachataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiControllerBase
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
        [HttpPost("loginadmin")]
        public IActionResult LoginAdmin(LoginRequestDto request)
        {
            if (!_userService.ValidarUsuario(request, out User usuario))
                return UnauthorizedResponse("Credenciales inválidas");

            var token = _jwtService.GenerateToken(usuario.Id.ToString(), usuario.Email);

            return OkResponse(new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes) // o usar desde JwtSettings si preferís
            });
            
            
        }




        /// <summary>
        /// Login de un usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);
            if (user is null)
                return UnauthorizedResponse("Credenciales inválidas");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return UnauthorizedResponse("Credenciales inválidas");

            // 1. Generar access token
            var accessToken = _jwtService.GenerateToken(user.Id, user.Email);

            // 2. Generar refresh token
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpireAt = DateTime.UtcNow.AddDays(7)
            };

            // 3. Guardar refresh token
            await _userService.CreateRefreshTokenAsync(refreshToken);

            // 4. Devolver ambos tokens
            return OkResponse(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }



        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequestResponse(ModelState);

            try
            {
                await _userService.CreateUserAsync(dto);
                return OkResponse("Usuario registrado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }



        /// <summary>
        /// Listado de todos los usuarios
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        //[Authorize(Roles = "Admin")] // Opcional: solo para usuarios con rol Admin
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return OkResponse(users);
        }


    }
}
