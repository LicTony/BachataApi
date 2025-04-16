using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BachataApi.Models
{
    public class Paso
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required  string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public required int Orden { get; set; }
        public required int TiempoDesde { get; set; }
        public required int TiempoHasta { get; set; }
        public required string Detalle { get; set; } = string.Empty;
    }
}
