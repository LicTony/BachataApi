namespace BachataApi.DTOs
{
    public class UpdateFiguraDto
    {
        public required string Id { get; set; } = string.Empty;
        public required string Detalle { get; set; } = string.Empty;
        public required DateTime Fecha { get; set; }
    }
}
