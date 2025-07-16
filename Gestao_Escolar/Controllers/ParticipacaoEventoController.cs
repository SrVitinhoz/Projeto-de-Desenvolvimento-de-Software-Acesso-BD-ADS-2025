using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipacaoEventoController : ControllerBase
    {
        private readonly IParticipacaoEventoService _participacaoEventoService;

        public ParticipacaoEventoController(IParticipacaoEventoService participacaoEventoService)
        {
            _participacaoEventoService = participacaoEventoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipacaoEventoDTO>>> GetAll()
        {
            var participacoes = await _participacaoEventoService.GetAllAsync();
            return Ok(participacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipacaoEventoDTO>> GetById(int id)
        {
            var participacao = await _participacaoEventoService.GetByIdAsync(id);
            if (participacao == null)
                return NotFound();

            return Ok(participacao);
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<ActionResult<IEnumerable<ParticipacaoEventoDTO>>> GetByEvento(int eventoId)
        {
            var participacoes = await _participacaoEventoService.GetByEventoIdAsync(eventoId);
            return Ok(participacoes);
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult<IEnumerable<ParticipacaoEventoDTO>>> GetByAluno(int alunoId)
        {
            var participacoes = await _participacaoEventoService.GetByAlunoIdAsync(alunoId);
            return Ok(participacoes);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ParticipacaoEventoUpdateDTO participacaoDto)
        {
            var participacaoAtualizada = await _participacaoEventoService.UpdateAsync(id, participacaoDto);
            if (participacaoAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _participacaoEventoService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }


    }



    public class RegistrarParticipacaoDTO
    {
        public int EventoId { get; set; }
        public int AlunoId { get; set; }
    }
}