using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculaController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatriculaDTO>>> GetAll()
        {
            var matriculas = await _matriculaService.GetAllAsync();
            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatriculaDTO>> GetById(int id)
        {
            var matricula = await _matriculaService.GetByIdAsync(id);
            if (matricula == null)
                return NotFound();

            return Ok(matricula);
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult<IEnumerable<MatriculaDTO>>> GetByAluno(int alunoId)
        {
            var matriculas = await _matriculaService.GetByAlunoIdAsync(alunoId);
            return Ok(matriculas);
        }

        [HttpPost]
        public async Task<ActionResult<MatriculaDTO>> Create(MatriculaCreateDTO matriculaDto)
        {
            var novaMatricula = await _matriculaService.CreateAsync(matriculaDto);
            return CreatedAtAction(nameof(GetById), new { id = novaMatricula.Id }, novaMatricula);
        }

        [HttpPost("atualizar-status")]
        public async Task<ActionResult<MatriculaDTO>> AtualizarStatus(
            [FromBody] AtualizarStatusMatriculaDTO atualizarDto)
        {
            try
            {
                var matricula = await _matriculaService.AtualizarStatusMatriculaAsync(
                    atualizarDto.AlunoId,
                    atualizarDto.Status,
                    atualizarDto.Descricao,
                    atualizarDto.FuncionarioId);

                if (matricula == null)
                    return BadRequest("Não foi possível atualizar o status da matrícula");

                return CreatedAtAction(nameof(GetById), new { id = matricula.Id }, matricula);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MatriculaUpdateDTO matriculaDto)
        {
            var matriculaAtualizada = await _matriculaService.UpdateAsync(id, matriculaDto);
            if (matriculaAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _matriculaService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }

    public class AtualizarStatusMatriculaDTO
    {
        public int AlunoId { get; set; }
        public string Status { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public int? FuncionarioId { get; set; }
    }
}