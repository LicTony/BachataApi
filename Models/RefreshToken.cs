namespace BachataApi.Models
{
    public class RefreshToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Mongo usa string por default
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public bool IsRevoked { get; set; } = false;
    }
}
