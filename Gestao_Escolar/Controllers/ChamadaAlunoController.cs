using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChamadaAlunoController : ControllerBase
    {
        private readonly IChamadaAlunoService _chamadaAlunoService;

        public ChamadaAlunoController(IChamadaAlunoService chamadaAlunoService)
        {
            _chamadaAlunoService = chamadaAlunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChamadaAlunoDTO>>> GetAll()
        {
            var chamadasAluno = await _chamadaAlunoService.GetAllAsync();
            return Ok(chamadasAluno);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChamadaAlunoDTO>> GetById(int id)
        {
            var chamadaAluno = await _chamadaAlunoService.GetByIdAsync(id);
            if (chamadaAluno == null)
                return NotFound();

            return Ok(chamadaAluno);
        }

        [HttpGet("chamada/{chamadaId}")]
        public async Task<ActionResult<IEnumerable<ChamadaAlunoDTO>>> GetByChamada(int chamadaId)
        {
            var chamadasAluno = await _chamadaAlunoService.GetByChamadaIdAsync(chamadaId);
            return Ok(chamadasAluno);
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<ActionResult<IEnumerable<ChamadaAlunoDTO>>> GetByAluno(int alunoId)
        {
            var chamadasAluno = await _chamadaAlunoService.GetByAlunoIdAsync(alunoId);
            return Ok(chamadasAluno);
        }

        [HttpPost]
        public async Task<ActionResult<ChamadaAlunoDTO>> Create(ChamadaAlunoCreateDTO chamadaAlunoDto)
        {
            var novaChamadaAluno = await _chamadaAlunoService.CreateAsync(chamadaAlunoDto);
            return CreatedAtAction(nameof(GetById), new { id = novaChamadaAluno.Id }, novaChamadaAluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ChamadaAlunoUpdateDTO chamadaAlunoDto)
        {
            var chamadaAlunoAtualizada = await _chamadaAlunoService.UpdateAsync(id, chamadaAlunoDto);
            if (chamadaAlunoAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _chamadaAlunoService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}