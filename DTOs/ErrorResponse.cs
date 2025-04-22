namespace BachataApi.DTOs
{
    public class ErrorResponse
    {
        public string? Message { get; set; } // Mensaje general opcional
        public int? StatusCode { get; set; } // Código HTTP opcional
        public List<ErrorItem>? Errors { get; set; } // Lista de errores de validación

        // Nuevo campo para detalles adicionales
        public object? Details { get; set; }
    }

    public class ErrorItem
    {
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
