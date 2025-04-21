using BachataApi.Models;
using BachataApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BachataApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PruebasController : ControllerBase
    {
        // GET: api/pruebas


        /// <summary>
        /// Obtiene un mensaje de prueba
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ObtenerMensaje()
        {
            return Ok("¡Hola desde el controlador de pruebas!");
        }



        /// <summary>
        /// Este contenido solo es visible con JWT válido
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("privado")]
        public IActionResult GetPrivado()
        {
            return Ok("Este contenido solo es visible con JWT válido");
        }


        /// <summary>
        /// Rebice un parametro un numero entero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/pruebas/5
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            return Ok($"Has solicitado el ID: {id}");
        }

        // POST: api/pruebas

        /// <summary>
        /// Recibo un paramero en el Body
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Crear([FromBody] string valor)
        {
            return Ok($"Valor recibido: {valor}");
        }

        // PUT: api/pruebas/5
        /// <summary>
        /// Recibe un valor por Get y otro por el Body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nuevoValor"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] string nuevoValor)
        {
            return Ok($"ID {id} actualizado con valor: {nuevoValor}");
        }

        // DELETE: api/pruebas/5
        /// <summary>
        /// Recibe un paremetro por Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return Ok($"ID {id} eliminado");
        }

        // NUEVO: GET que lee una variable de entorno
        // GET: api/pruebas/entorno
        /// <summary>
        /// GET que lee una variable de entorno
        /// </summary>
        /// <returns></returns>
        [HttpGet("entorno")]
        public IActionResult LeerVariableEntorno()
        {
            var valor = Environment.GetEnvironmentVariable("MI_VARIABLE_PRUEBA");
            if (string.IsNullOrEmpty(valor))
                return NotFound("La variable de entorno 'MI_VARIABLE_PRUEBA' no está definida.");

            return Ok($"Valor de MI_VARIABLE_PRUEBA: {valor}");
        }

        /// <summary>
        /// GET que lee una variable de entorno que le paso por parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
