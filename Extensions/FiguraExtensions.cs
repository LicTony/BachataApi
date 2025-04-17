using BachataApi.DTOs;
using BachataApi.Models;
using MongoDB.Bson;

namespace BachataApi.Extensions
{
    public static class FiguraExtensions
    {
        public static Figura ToModel(this UpdateFiguraDto dto)
        {
            return new Figura
            {
                Id = dto.Id,
                Detalle = dto.Detalle,
                Fecha = dto.Fecha,
                Pasos = []
            };
        }


        public static Figura ToModel(this CreateFiguraDto dto)
        {
            return new Figura
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Detalle = dto.Detalle,
                Fecha = dto.Fecha,
                Pasos = []
            };
        }



        public static CreateFiguraDto ToCreateDto(this Figura figura)
        {
            return new CreateFiguraDto
            {   
                Detalle = figura.Detalle,
                Fecha = figura.Fecha
            };
        }

        public static UpdateFiguraDto ToUpdateDto(this Figura figura)
        {
            return new UpdateFiguraDto
            {
                Id = figura.Id ?? "",
                Detalle = figura.Detalle,
                Fecha = figura.Fecha
            };
        }
    }
}
