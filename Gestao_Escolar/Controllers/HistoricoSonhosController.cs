using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoricoSonhosController : ControllerBase
    {
        private readonly IHistoricoSonhosService _historicoSonhosService;

        public HistoricoSonhosController(IHistoricoSonhosService historicoSonhosService)
        {
            _historicoSonhosService = historicoSonhosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoSonhosDTO>>> GetAll()
        {
            var historicos = await _historicoSonhosService.GetAllAsync();
            return Ok(historicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoSonhosDTO>> GetById(int id)
        {
            var historico = await _historicoSonhosService.GetByIdAsync(id);
            if (historico == null)
                return NotFound();

            return Ok(historico);
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult<IEnumerable<HistoricoSonhosDTO>>> GetByAluno(int alunoId)
        {
            var historicos = await _historicoSonhosService.GetByAlunoIdAsync(alunoId);
            return Ok(historicos);
        }

        [HttpPost]
        public async Task<ActionResult<HistoricoSonhosDTO>> Create(HistoricoSonhosCreateDTO historicoDto)
        {
            var novoHistorico = await _historicoSonhosService.CreateAsync(historicoDto);
            return CreatedAtAction(nameof(GetById), new { id = novoHistorico.Id }, novoHistorico);
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult<HistoricoSonhosDTO>> AdicionarSonhos(
            [FromBody] AdicionarSonhosDTO adicionarDto)
        {
            try
            {
                var historico = await _historicoSonhosService.AdicionarSonhosAsync(
                    adicionarDto.AlunoId,
                    adicionarDto.Valor,
                    adicionarDto.Motivo,
                    adicionarDto.FuncionarioId);

                return CreatedAtAction(nameof(GetById), new { id = historico.Id }, historico);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("subtrair")]
        public async Task<ActionResult<HistoricoSonhosDTO>> SubtrairSonhos(
            [FromBody] SubtrairSonhosDTO subtrairDto)
        {
            try
            {
                var historico = await _historicoSonhosService.SubtrairSonhosAsync(
                    subtrairDto.AlunoId,
                    subtrairDto.Valor,
                    subtrairDto.Motivo,
                    subtrairDto.FuncionarioId);

                if (historico == null)
                    return BadRequest("Saldo insuficiente");

                return CreatedAtAction(nameof(GetById), new { id = historico.Id }, historico);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HistoricoSonhosUpdateDTO historicoDto)
        {
            var historicoAtualizado = await _historicoSonhosService.UpdateAsync(id, historicoDto);
            if (historicoAtualizado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _historicoSonhosService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }

    public class AdicionarSonhosDTO
    {
        public int AlunoId { get; set; }
        public int Valor { get; set; }
        public string Motivo { get; set; } = null!;
        public int? FuncionarioId { get; set; }
    }

    public class SubtrairSonhosDTO
    {
        public int AlunoId { get; set; }
        public int Valor { get; set; }
        public string Motivo { get; set; } = null!;
        public int? FuncionarioId { get; set; }
    }
}