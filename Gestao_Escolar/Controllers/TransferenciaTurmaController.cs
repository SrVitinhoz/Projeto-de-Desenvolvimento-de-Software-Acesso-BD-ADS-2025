using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenciaTurmaController : ControllerBase
    {
        private readonly ITransferenciaTurmaService _transferenciaTurmaService;

        public TransferenciaTurmaController(ITransferenciaTurmaService transferenciaTurmaService)
        {
            _transferenciaTurmaService = transferenciaTurmaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferenciaTurmaDTO>>> GetAll()
        {
            var transferencias = await _transferenciaTurmaService.GetAllAsync();
            return Ok(transferencias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransferenciaTurmaDTO>> GetById(int id)
        {
            var transferencia = await _transferenciaTurmaService.GetByIdAsync(id);
            if (transferencia == null)
                return NotFound();

            return Ok(transferencia);
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult<IEnumerable<TransferenciaTurmaDTO>>> GetByAluno(int alunoId)
        {
            var transferencias = await _transferenciaTurmaService.GetByAlunoIdAsync(alunoId);
            return Ok(transferencias);
        }

        [HttpPost]
        public async Task<ActionResult<TransferenciaTurmaDTO>> Create(TransferenciaTurmaCreateDTO transferenciaDto)
        {
            var novaTransferencia = await _transferenciaTurmaService.CreateAsync(transferenciaDto);
            return CreatedAtAction(nameof(GetById), new { id = novaTransferencia.Id }, novaTransferencia);
        }

        [HttpPost("transferir")]
        public async Task<ActionResult<TransferenciaTurmaDTO>> RealizarTransferencia(
            [FromBody] TransferenciaTurmaCreateDTO transferenciaDto)
        {
            try
            {
                var transferencia = await _transferenciaTurmaService.RealizarTransferenciaAsync(transferenciaDto);
                if (transferencia == null)
                    return BadRequest("Não foi possível realizar a transferência");

                return CreatedAtAction(nameof(GetById), new { id = transferencia.Id }, transferencia);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransferenciaTurmaUpdateDTO transferenciaDto)
        {
            var transferenciaAtualizada = await _transferenciaTurmaService.UpdateAsync(id, transferenciaDto);
            if (transferenciaAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _transferenciaTurmaService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}