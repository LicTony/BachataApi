using BachataApi.Configuration;
using BachataApi.DTOs;
using BachataApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Data;

namespace BachataApi.Services
{
    public class UserService
    {

        private readonly UserSettings _userSettings;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<RefreshToken> _refreshTokens;

        public UserService(IOptions<UserSettings> userOptions , IOptions<MongoDbSettings> mongoDbSettings)
        {
            _userSettings = userOptions.Value;

            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _users = database.GetCollection<User>("Users");
            _refreshTokens = database.GetCollection<RefreshToken>("RefreshTokens");
        }

        public bool ValidarUsuario(LoginRequestDto loginRequest , out User usuario) 
        {
            usuario = new User();


            // Usuario simulado (en el futuro: validar desde base de datos)
            if (loginRequest.Email == _userSettings.Email && loginRequest.Password == _userSettings.Password)
            {
                usuario.Id = _userSettings.Id.ToString();
                usuario.Email = _userSettings.Email;
                usuario.PasswordHash = "";
                return true;
            }


            return false;
        }


        // Métodos de usuarios
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }



        public async Task CreateUserAsync(RegisterUserDto dto)
        {
            var existing = await GetByEmailAsync(dto.Email);
            if (existing is not null)
                throw new  DuplicateNameException("El usuario ya existe");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _users.InsertOneAsync(user);
        }

        // Métodos de refresh token
        public async Task CreateRefreshTokenAsync(RefreshToken token)
        {
            await _refreshTokens.InsertOneAsync(token);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _refreshTokens.Find(x => x.Token == token && !x.IsRevoked).FirstOrDefaultAsync();
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            var update = Builders<RefreshToken>.Update.Set(x => x.IsRevoked, true);
            await _refreshTokens.UpdateOneAsync(x => x.Token == token, update);
        }




        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }



    }
}
