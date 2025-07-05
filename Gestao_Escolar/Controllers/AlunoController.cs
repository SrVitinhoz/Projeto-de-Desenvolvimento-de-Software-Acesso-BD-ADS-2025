using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAll()
        {
            var alunos = await _alunoService.GetAllAsync();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetById(int id)
        {
            var aluno = await _alunoService.GetByIdAsync(id);
            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

        [HttpGet("turma/{turmaId}")]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetByTurma(int turmaId)
        {
            var alunos = await _alunoService.GetAlunosByTurmaAsync(turmaId);
            return Ok(alunos);
        }

        [HttpPost]
        public async Task<ActionResult<AlunoDTO>> Create(AlunoCreateDTO alunoDto)
        {
            var novoAluno = await _alunoService.CreateAsync(alunoDto);
            return CreatedAtAction(nameof(GetById), new { id = novoAluno.Id }, novoAluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AlunoUpdateDTO alunoDto)
        {
            var alunoAtualizado = await _alunoService.UpdateAsync(id, alunoDto);
            if (alunoAtualizado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _alunoService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/saldo-sonhos")]
        public async Task<IActionResult> AtualizarSaldoSonhos(int id, [FromBody] int valor)
        {
            var resultado = await _alunoService.AtualizarSaldoSonhosAsync(id, valor);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}