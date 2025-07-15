using Microsoft.AspNetCore.Mvc;
using Gestao_Escolar.Services;
using Gestao_Escolar.DTOs;

namespace Gestao_Escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _FuncionarioService;

        public FuncionarioController(IFuncionarioService FuncionarioService)
        {
            _FuncionarioService = FuncionarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> GetAll()
        {
            var Funcionarios = await _FuncionarioService.GetAllAsync();
            return Ok(Funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioDTO>> GetById(int id)
        {
            var Funcionario = await _FuncionarioService.GetByIdAsync(id);
            if (Funcionario == null)
                return NotFound();

            return Ok(Funcionario);
        }

        [HttpGet("professores")]
        public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> GetProfessores()
        {
            var professores = await _FuncionarioService.GetProfessoresAtivosAsync();
            return Ok(professores);
        }

        [HttpGet("verificar-cpf/{cpf}")]
        public async Task<ActionResult<bool>> VerificarCpf(string cpf)
        {
            var existe = await _FuncionarioService.VerificarCpfExistenteAsync(cpf);
            return Ok(existe);
        }

        [HttpPost]
        public async Task<ActionResult<FuncionarioDTO>> Create(FuncionarioCreateDTO FuncionarioDto)
        {
            try
            {
                var novoFuncionario = await _FuncionarioService.CreateAsync(FuncionarioDto);
                return CreatedAtAction(nameof(GetById), new { id = novoFuncionario.Id }, novoFuncionario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FuncionarioUpdateDTO FuncionarioDto)
        {
            var FuncionarioAtualizado = await _FuncionarioService.UpdateAsync(id, FuncionarioDto);
            if (FuncionarioAtualizado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _FuncionarioService.DeleteAsync(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("login/{cpf}")]
        public async Task<ActionResult<FuncionarioLoginDTO>> GetLogin(string cpf)
        {
            var loginInfo = await _FuncionarioService.GetLoginByCpfAsync(cpf);
            if (loginInfo == null)
                return NotFound("Funcionário não encontrado");

            return Ok(loginInfo);
        }
    }
}