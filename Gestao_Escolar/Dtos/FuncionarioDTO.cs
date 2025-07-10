using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O CPF do funcionário é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter 14 caracteres")]
        public string Cpf { get; set; } = null!;

        [Required(ErrorMessage = "O cargo do funcionário é obrigatório")]
        public string Cargo { get; set; } = null!;

        [Required(ErrorMessage = "O status do funcionário é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "A data de admissão é obrigatória")]
        public DateTime DataAdmissao { get; set; }

        public DateTime? DataDesligamento { get; set; }

        public int? MateriaId { get; set; }

        public string? MateriaNome { get; set; }


    }

    public class FuncionarioCreateDTO
    {
        [Required(ErrorMessage = "O nome do funcionário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O CPF do funcionário é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter 14 caracteres")]
        public string Cpf { get; set; } = null!;

        [Required(ErrorMessage = "O cargo do funcionário é obrigatório")]
        public string Cargo { get; set; } = null!;

        [Required(ErrorMessage = "O status do funcionário é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "A data de admissão é obrigatória")]
        public DateTime DataAdimicao { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres")]
        public string Senha { get; set; } = null!;

        public int? MateriaId { get; set; }
    }

    public class FuncionarioUpdateDTO
    {
        [Required(ErrorMessage = "O nome do funcionário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O cargo do funcionário é obrigatório")]
        public string Cargo { get; set; } = null!;

        [Required(ErrorMessage = "O status do funcionário é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "A data de admissão é obrigatória")]
        public DateTime DataAdmissao { get; set; }

        public DateTime? DataDesligamento { get; set; }


        [StringLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres")]
        public string? Senha { get; set; }

        public int? MateriaId { get; set; }
    }
}