using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace BachataApi.Models
{
    public class Figura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public required string Detalle { get; set; } = string.Empty;
        public required DateTime Fecha { get; set; }

        public List<Paso> Pasos { get; set; } = [];
    }
}
