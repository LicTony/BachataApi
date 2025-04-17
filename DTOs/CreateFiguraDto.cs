namespace BachataApi.DTOs
{
    public class CreateFiguraDto
    {
        public required string Detalle { get; set; } = string.Empty;
        public required DateTime Fecha { get; set; }
    }
}
