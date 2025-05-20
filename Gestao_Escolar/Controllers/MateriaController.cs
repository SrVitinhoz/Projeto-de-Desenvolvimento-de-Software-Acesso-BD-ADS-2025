using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> GetAll()
        {
            var materias = await _materiaService.GetAllAsync();
            return Ok(materias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetById(int id)
        {
            var materia = await _materiaService.GetByIdAsync(id);
            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        [HttpGet("ativas")]
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> GetAtivas()
        {
            var materias = await _materiaService.GetMateriasAtivasAsync();
            return Ok(materias);
        }

        [HttpPost]
        public async Task<ActionResult<MateriaDTO>> Create(MateriaCreateDTO materiaDto)
        {
            var novaMateria = await _materiaService.CreateAsync(materiaDto);
            return CreatedAtAction(nameof(GetById), new { id = novaMateria.Id }, novaMateria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MateriaUpdateDTO materiaDto)
        {
            var materiaAtualizada = await _materiaService.UpdateAsync(id, materiaDto);
            if (materiaAtualizada == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _materiaService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}