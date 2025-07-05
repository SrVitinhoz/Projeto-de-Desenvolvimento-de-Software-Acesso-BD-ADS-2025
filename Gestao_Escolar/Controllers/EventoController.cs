using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetAll()
        {
            var eventos = await _eventoService.GetAllAsync();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDTO>> GetById(int id)
        {
            var evento = await _eventoService.GetByIdAsync(id);
            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetAtivos()
        {
            var eventos = await _eventoService.GetEventosAtivosAsync();
            return Ok(eventos);
        }

        [HttpPost]
        public async Task<ActionResult<EventoDTO>> Create(EventoCreateDTO eventoDto)
        {
            var novoEvento = await _eventoService.CreateAsync(eventoDto);
            return CreatedAtAction(nameof(GetById), new { id = novoEvento.Id }, novoEvento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EventoUpdateDTO eventoDto)
        {
            var eventoAtualizado = await _eventoService.UpdateAsync(id, eventoDto);
            if (eventoAtualizado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _eventoService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{eventoId}/participacao/{alunoId}")]
        public async Task<IActionResult> RegistrarParticipacao(int eventoId, int alunoId)
        {
            var resultado = await _eventoService.RegistrarParticipacaoAsync(eventoId, alunoId);
            if (!resultado)
                return BadRequest("Não foi possível registrar a participação");

            return NoContent();
        }
    }
}