namespace BachataApi.DTOs
{
    public class UpdatetPasoDto
    {
        public required string Id { get; set; } = string.Empty;
        public required int Orden { get; set; }
        public required int TiempoDesde { get; set; }
        public required int TiempoHasta { get; set; }
        public required string Detalle { get; set; } = string.Empty;
    }
}
