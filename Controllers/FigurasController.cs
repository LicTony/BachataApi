using BachataApi.DTOs;
using BachataApi.Models;
using BachataApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BachataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FigurasController : ControllerBase
    {
        private readonly FiguraService _figuraService;

        public FigurasController(FiguraService figuraService)
        {
            _figuraService = figuraService;
        }



        /// <summary>
        /// Obtener todas las figuras con sus respectivos pasos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Figura>>> Get() =>
            await _figuraService.GetAllAsync();


        /// <summary>
        /// Obtener una figura en particular
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Figura>> Get(string id)
        {
            var figura = await _figuraService.GetByIdAsync(id);

            if (figura is null)
                return NotFound();

            return figura;
        }

        /// <summary>
        /// Agregar una figura
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateFiguraDto dto)
        {
            var figura = await _figuraService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = figura.Id }, figura);
          
        }
        
        /// <summary>
        /// Modificar una figura
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, UpdateFiguraDto dto)
        {
            var figura = await _figuraService.GetByIdAsync(id);
            if (figura is null)
                return NotFound();

            dto.Id = id;
            await _figuraService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Borrar una figura
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var figura = await _figuraService.GetByIdAsync(id);
            if (figura is null)
                return NotFound();

            await _figuraService.DeleteAsync(id);
            return NoContent();
        }


        // POST: api/figuras/{figuraId}/pasos
        /// <summary>
        /// Agregar un Paso
        /// </summary>
        /// <param name="figuraId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("{figuraId}/pasos")]
        public async Task<IActionResult> AddPaso(string figuraId, CreatePasoDto dto)
        {
            await _figuraService.AddPasoAsync(figuraId, dto);
            return NoContent();
        }

        // PUT: api/figuras/{figuraId}/pasos
        /// <summary>
        /// Actualizar un Paso
        /// </summary>
        /// <param name="figuraId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{figuraId}/pasos")]
        public async Task<IActionResult> UpdatePaso(string figuraId, UpdatetPasoDto dto)
        {
            await _figuraService.UpdatePasoAsync(figuraId, dto);
            return NoContent();
        }

        // DELETE: api/figuras/{figuraId}/pasos/{pasoId}
        /// <summary>
        /// Borrar un paso de una figura dada
        /// </summary>
        /// <param name="figuraId"></param>
        /// <param name="pasoId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{figuraId}/pasos/{pasoId}")]
        public async Task<IActionResult> DeletePaso(string figuraId, string pasoId)
        {
            await _figuraService.DeletePasoAsync(figuraId, pasoId);
            return NoContent();
        }

        // POST: api/figuras/{figuraId}/pasos/swap?paso1=abc&paso2=xyz
        /// <summary>
        /// Intercambiar el Orden de 2 pasos dados
        /// </summary>
        /// <param name="figuraId"></param>
        /// <param name="paso1"></param>
        /// <param name="paso2"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("{figuraId}/pasos/swap")]
        public async Task<IActionResult> SwapPasos(string figuraId, [FromQuery] string paso1, [FromQuery] string paso2)
        {
            await _figuraService.SwapPasosAsync(figuraId, paso1, paso2);
            return NoContent();
        }



        /// <summary>
        /// Retorna aleatoriamente una figura con sus respectivos pasos en formato json
        /// </summary>
        /// <returns>Una figura con sus respectivos pasos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("aleatoria")]
        public async Task<ActionResult<Figura>> GetFiguraAleatoria()
        {
            var figura = await _figuraService.GetRandomFiguraAsync();


            if (figura is null)
                return NoContent();

            return figura;

        }


        /// <summary>
        /// Retorna aleatoriamente una figura con sus respectivos pasos en formato HTML
        /// </summary>
        /// <returns>Una figura con sus respectivos pasos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("aleatoriahtml")]
        public async Task<IActionResult> GetFiguraAleatoriaHtml()
        {
            var figura = await _figuraService.GetRandomFiguraAsync();

            if (figura is null)
                return Content("<html><body><h1>No hay figuras disponibles.</h1></body></html>", "text/html");

            var pasosHtml = figura.Pasos
                .OrderBy(p => p.Orden)
                .Select(p => $@"
                    <li>
                        Tiempo: {p.TiempoDesde} a {p.TiempoHasta}
                        <br/>
                        <strong>{p.Detalle}</strong>
                    </li>")
                .ToList();

            var html = $@"
                    <html>
                        <head>
                            <title>Figura Aleatoria</title>
                            <style>
                                body {{ font-family: sans-serif; padding: 2rem; background-color: #f4f4f4; }}
                                h1 {{ color: #2c3e50; }}
                                ul {{ list-style: none; padding: 0; }}
                                li {{ background: white; margin-bottom: 1rem; padding: 1rem; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }}
                            </style>
                        </head>
                        <body>
                            <h1>Figura: {figura.Detalle}</h1>
                            <h3>Fecha: {figura.Fecha.ToShortDateString()}</h3>
                            <h2>Pasos</h2>
                            <ul>
                                {string.Join("", pasosHtml)}
                            </ul>
                        </body>
                    </html>";

            return Content(html, "text/html");
        }




    }
}
