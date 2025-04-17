﻿using BachataApi.DTOs;
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


        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateFiguraDto dto)
        {
            var figura = await _figuraService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = figura.Id }, figura);
          
        }
        
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("{figuraId}/pasos")]
        public async Task<IActionResult> AddPaso(string figuraId, CreatePasoDto dto)
        {
            await _figuraService.AddPasoAsync(figuraId, dto);
            return NoContent();
        }

        // PUT: api/figuras/{figuraId}/pasos
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{figuraId}/pasos")]
        public async Task<IActionResult> UpdatePaso(string figuraId, UpdatetPasoDto dto)
        {
            await _figuraService.UpdatePasoAsync(figuraId, dto);
            return NoContent();
        }

        // DELETE: api/figuras/{figuraId}/pasos/{pasoId}
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{figuraId}/pasos/{pasoId}")]
        public async Task<IActionResult> DeletePaso(string figuraId, string pasoId)
        {
            await _figuraService.DeletePasoAsync(figuraId, pasoId);
            return NoContent();
        }

        // POST: api/figuras/{figuraId}/pasos/swap?paso1=abc&paso2=xyz
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("{figuraId}/pasos/swap")]
        public async Task<IActionResult> SwapPasos(string figuraId, [FromQuery] string paso1, [FromQuery] string paso2)
        {
            await _figuraService.SwapPasosAsync(figuraId, paso1, paso2);
            return NoContent();
        }


    }
}
