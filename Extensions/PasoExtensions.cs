using BachataApi.DTOs;
using BachataApi.Models;
using MongoDB.Bson;

namespace BachataApi.Extensions
{
    public static class PasoExtensions
    {
        public static Paso ToModel(this UpdatetPasoDto dto)
        {
            return new Paso
            {
                Id = dto.Id,
                Orden = dto.Orden,
                TiempoDesde = dto.TiempoDesde,
                TiempoHasta = dto.TiempoHasta,
                Detalle = dto.Detalle
            };
        }


        public static Paso ToModel(this CreatePasoDto dto)
        {
            return new Paso
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Orden = dto.Orden,
                TiempoDesde = dto.TiempoDesde,
                TiempoHasta = dto.TiempoHasta,
                Detalle = dto.Detalle
            };
        }



        public static CreatePasoDto ToCreateDto(this Paso paso)
        {
            return new CreatePasoDto
            {
                Orden = paso.Orden,
                TiempoDesde = paso.TiempoDesde,
                TiempoHasta = paso.TiempoHasta,
                Detalle = paso.Detalle
            };
        }

        public static UpdatetPasoDto ToUpdateDto(this Paso paso)
        {
            return new UpdatetPasoDto
            {
                Id = paso.Id,
                Orden = paso.Orden,
                TiempoDesde = paso.TiempoDesde,
                TiempoHasta = paso.TiempoHasta,
                Detalle = paso.Detalle
            };
        }


    }
}
