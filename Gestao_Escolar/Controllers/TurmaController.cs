using Gestao_Escolar.DTOs;
using Gestao_Escolar.Models;
using Gestao_Escolar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurmaDTO>>> GetAll()
        {
            var turmas = await _turmaService.GetAllAsync();
            return Ok(turmas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TurmaDTO>> GetById(int id)
        {
            var turma = await _turmaService.GetByIdAsync(id);
            if (turma == null)
                return NotFound();

            return Ok(turma);
        }

        [HttpGet("periodo/{periodo}")]
        public async Task<ActionResult<IEnumerable<TurmaDTO>>> GetByPeriodo(string periodo)
        {
            var turmas = await _turmaService.GetTurmasAtivasByPeriodoAsync(periodo);
            return Ok(turmas);
        }


        [HttpPost]
        public async Task<ActionResult<TurmaDTO>> Create(TurmaCreateDTO turmaDto)
        {
            var novaTurma = await _turmaService.CreateAsync(turmaDto);
            return CreatedAtAction(nameof(GetById), new { id = novaTurma.Id }, novaTurma);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TurmaUpdateDTO turmaDto)
        {
            var turmaAtualizada = await _turmaService.UpdateAsync(id, turmaDto);
            if (turmaAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _turmaService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}