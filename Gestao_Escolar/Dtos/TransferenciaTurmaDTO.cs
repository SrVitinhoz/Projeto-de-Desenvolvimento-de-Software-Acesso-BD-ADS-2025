using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class TransferenciaTurmaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        public string? AlunoNome { get; set; }

        public int? TurmaOrigemId { get; set; }

        public string? TurmaOrigemNome { get; set; }

        public int? TurmaDestinoId { get; set; }

        public string? TurmaDestinoNome { get; set; }

        [Required(ErrorMessage = "A data de transferência é obrigatória")]
        public DateTime DataTransferencia { get; set; }

        public int? FuncionarioId { get; set; }

        public string? FuncionarioNome { get; set; }
    }

    public class TransferenciaTurmaCreateDTO
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        public int? TurmaOrigemId { get; set; }

        public int? TurmaDestinoId { get; set; }

        [Required(ErrorMessage = "A data de transferência é obrigatória")]
        public DateTime DataTransferencia { get; set; }

        public int? FuncionarioId { get; set; }
    }

    public class TransferenciaTurmaUpdateDTO
    {
        public int? TurmaDestinoId { get; set; }

        [Required(ErrorMessage = "A data de transferência é obrigatória")]
        public DateTime DataTransferencia { get; set; }
    }
}