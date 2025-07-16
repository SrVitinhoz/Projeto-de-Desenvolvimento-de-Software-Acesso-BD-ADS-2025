using Gestao_Escolar.DTOs;
using Gestao_Escolar.Models;
using Gestao_Escolar.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChamadaController : ControllerBase
    {
        private readonly IChamadaService _chamadaService;
        private readonly IChamadaAlunoService _chamadaAlunoService;

        public ChamadaController(IChamadaService chamadaService, IChamadaAlunoService chamadaAlunoService)
        {
            _chamadaService = chamadaService;
            _chamadaAlunoService = chamadaAlunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChamadaDTO>>> GetAll()
        {
            var chamadas = await _chamadaService.GetAllAsync();
            return Ok(chamadas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChamadaDTO>> GetById(int id)
        {
            var chamada = await _chamadaService.GetByIdAsync(id);
            if (chamada == null)
                return NotFound();

            return Ok(chamada);
        }

        [HttpPost]
        public async Task<ActionResult<ChamadaDTO>> Create(ChamadaCreateDTO chamadaDto)
        {
            var novaChamada = await _chamadaService.CreateAsync(chamadaDto);
            return CreatedAtAction(nameof(GetById), new { id = novaChamada.Id }, novaChamada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ChamadaUpdateDTO chamadaDto)
        {
            var chamadaAtualizada = await _chamadaService.UpdateAsync(id, chamadaDto);
            if (chamadaAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _chamadaService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        public class ChamadaCompletaDTO
        {
            public ChamadaCreateDTO Chamada { get; set; } = null!;
            public List<ChamadaAlunoCreateDTO> Presencas { get; set; } = null!;
        }
    }
}