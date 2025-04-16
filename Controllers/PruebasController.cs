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

        // NUEVO: GET que lee una variable de entorno
        // GET: api/pruebas/entorno
        [HttpGet("entorno")]
        public IActionResult LeerVariableEntorno()
        {
            var valor = Environment.GetEnvironmentVariable("MI_VARIABLE_PRUEBA");
            if (string.IsNullOrEmpty(valor))
                return NotFound("La variable de entorno 'MI_VARIABLE_PRUEBA' no está definida.");

            return Ok($"Valor de MI_VARIABLE_PRUEBA: {valor}");
        }

        [HttpGet("entorno/{id}")]
        public IActionResult LeerVariableEntornoParam(string id)
        {
            var valor = Environment.GetEnvironmentVariable(id);
            if (string.IsNullOrEmpty(valor))
                return NotFound($"La variable de entorno '{id}' no está definida.");

            return Ok($"Valor de {id}: {valor}");
        }

    }
}
