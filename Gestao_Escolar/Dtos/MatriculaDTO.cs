using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class MatriculaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        public string? AlunoNome { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O status da matrícula é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres")]
        public string Descricao { get; set; } = null!;

        public int? FuncionarioId { get; set; }

        public string? FuncionarioNome { get; set; }
    }

    public class MatriculaCreateDTO
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "O status da matrícula é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres")]
        public string Descricao { get; set; } = null!;

        public int? FuncionarioId { get; set; }
    }

    public class MatriculaUpdateDTO
    {
        [Required(ErrorMessage = "O status da matrícula é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres")]
        public string Descricao { get; set; } = null!;
    }
}