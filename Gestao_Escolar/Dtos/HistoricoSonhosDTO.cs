using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class HistoricoSonhosDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        public string? AlunoNome { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O tipo de operação é obrigatório")]
        public string Tipo { get; set; } = null!;

        public string? Motivo { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public int Valor { get; set; }

        public int? FuncionarioId { get; set; }

        public string? FuncionarioNome { get; set; }
    }

    public class HistoricoSonhosCreateDTO
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "O tipo de operação é obrigatório")]
        public string Tipo { get; set; } = null!;

        public string? Motivo { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public int Valor { get; set; }

        public int? FuncionarioId { get; set; }
    }

    public class HistoricoSonhosUpdateDTO
    {
        public string? Motivo { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public int Valor { get; set; }
    }
}