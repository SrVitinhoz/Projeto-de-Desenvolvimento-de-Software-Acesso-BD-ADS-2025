using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        public int? TurmaId { get; set; }

        public string? TurmaNome { get; set; }

        [Required(ErrorMessage = "O status da matrícula é obrigatório")]
        public string StatusMatricula { get; set; } = null!;

        public int SaldoSonhos { get; set; }

        [Required(ErrorMessage = "O período é obrigatório")]
        public string Periodo { get; set; } = null!;
    }

    public class AlunoCreateDTO
    {
        [Required(ErrorMessage = "O nome do aluno é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        public int? TurmaId { get; set; }

        [Required(ErrorMessage = "O status da matrícula é obrigatório")]
        public string StatusMatricula { get; set; } = null!;

        [Required(ErrorMessage = "O período é obrigatório")]
        public string Periodo { get; set; } = null!;
    }

    public class AlunoUpdateDTO
    {
        [Required(ErrorMessage = "O nome do aluno é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        public int? TurmaId { get; set; }

        [Required(ErrorMessage = "O status da matrícula é obrigatório")]
        public string StatusMatricula { get; set; } = null!;

        [Required(ErrorMessage = "O período é obrigatório")]
        public string Periodo { get; set; } = null!;
    }
}