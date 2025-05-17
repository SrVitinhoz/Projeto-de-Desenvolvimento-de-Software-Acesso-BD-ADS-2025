using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class ChamadaAlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID da chamada é obrigatório")]
        public int ChamadaId { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        public string? AlunoNome { get; set; }

        [Required(ErrorMessage = "O status de presença é obrigatório")]
        public string Status { get; set; } = null!;

        public string? Observacao { get; set; }
    }

    public class ChamadaAlunoCreateDTO
    {
        [Required(ErrorMessage = "O ID da chamada é obrigatório")]
        public int ChamadaId { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "O status de presença é obrigatório")]
        public string Status { get; set; } = null!;

        public string? Observacao { get; set; }
    }

    public class ChamadaAlunoUpdateDTO
    {
        [Required(ErrorMessage = "O status de presença é obrigatório")]
        public string Status { get; set; } = null!;

        public string? Observacao { get; set; }
    }
}