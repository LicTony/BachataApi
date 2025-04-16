using BachataApi.Models;
using BachataApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BachataApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PruebasController : ControllerBase
    {
        // GET: api/pruebas
        [HttpGet]
        public IActionResult ObtenerMensaje()
        {
            return Ok("¡Hola desde el controlador de pruebas!");
        }

        // GET: api/pruebas/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            return Ok($"Has solicitado el ID: {id}");
        }

        // POST: api/pruebas
        [HttpPost]
        public IActionResult Crear([FromBody] string valor)
        {
            return Ok($"Valor recibido: {valor}");
        }

        // PUT: api/pruebas/5
        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] string nuevoValor)
        {
            return Ok($"ID {id} actualizado con valor: {nuevoValor}");
        }

        // DELETE: api/pruebas/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return Ok($"ID {id} eliminado");
        }


    }
}
