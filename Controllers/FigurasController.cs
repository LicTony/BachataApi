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

        [HttpGet]
        public async Task<ActionResult<List<Figura>>> Get() =>
            await _figuraService.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Figura>> Get(string id)
        {
            var figura = await _figuraService.GetByIdAsync(id);

            if (figura is null)
                return NotFound();

            return figura;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Figura figura)
        {
            await _figuraService.CreateAsync(figura);
            return CreatedAtAction(nameof(Get), new { id = figura.Id }, figura);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, Figura updatedFigura)
        {
            var figura = await _figuraService.GetByIdAsync(id);
            if (figura is null)
                return NotFound();

            updatedFigura.Id = id;
            await _figuraService.UpdateAsync(id, updatedFigura);
            return NoContent();
        }

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
        [HttpPost("{figuraId}/pasos")]
        public async Task<IActionResult> AddPaso(string figuraId, Paso paso)
        {
            await _figuraService.AddPasoAsync(figuraId, paso);
            return NoContent();
        }

        // PUT: api/figuras/{figuraId}/pasos
        [HttpPut("{figuraId}/pasos")]
        public async Task<IActionResult> UpdatePaso(string figuraId, Paso paso)
        {
            await _figuraService.UpdatePasoAsync(figuraId, paso);
            return NoContent();
        }

        // DELETE: api/figuras/{figuraId}/pasos/{pasoId}
        [HttpDelete("{figuraId}/pasos/{pasoId}")]
        public async Task<IActionResult> DeletePaso(string figuraId, string pasoId)
        {
            await _figuraService.DeletePasoAsync(figuraId, pasoId);
            return NoContent();
        }

        // POST: api/figuras/{figuraId}/pasos/swap?paso1=abc&paso2=xyz
        [HttpPost("{figuraId}/pasos/swap")]
        public async Task<IActionResult> SwapPasos(string figuraId, [FromQuery] string paso1, [FromQuery] string paso2)
        {
            await _figuraService.SwapPasosAsync(figuraId, paso1, paso2);
            return NoContent();
        }


    }
}
