using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> GetAll()
        {
            var funcionarios = await _funcionarioService.GetAllAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioDTO>> GetById(int id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound();

            return Ok(funcionario);
        }

        [HttpGet("professores")]
        public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> GetProfessores()
        {
            var professores = await _funcionarioService.GetProfessoresAtivosAsync();
            return Ok(professores);
        }

        [HttpGet("verificar-cpf/{cpf}")]
        public async Task<ActionResult<bool>> VerificarCpf(string cpf)
        {
            var existe = await _funcionarioService.VerificarCpfExistenteAsync(cpf);
            return Ok(existe);
        }

        [HttpPost]
        public async Task<ActionResult<FuncionarioDTO>> Create(FuncionarioCreateDTO funcionarioDto)
        {
            try
            {
                var novoFuncionario = await _funcionarioService.CreateAsync(funcionarioDto);
                return CreatedAtAction(nameof(GetById), new { id = novoFuncionario.Id }, novoFuncionario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FuncionarioUpdateDTO funcionarioDto)
        {
            var funcionarioAtualizado = await _funcionarioService.UpdateAsync(id, funcionarioDto);
            if (funcionarioAtualizado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _funcionarioService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }
    }
}