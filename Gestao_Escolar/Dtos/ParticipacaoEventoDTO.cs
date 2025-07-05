using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class ParticipacaoEventoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        public string? AlunoNome { get; set; }

        [Required(ErrorMessage = "O ID do evento é obrigatório")]
        public int EventoId { get; set; }

        public string? EventoNome { get; set; }

        public bool Participou { get; set; }

    }

    public class ParticipacaoEventoCreateDTO
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "O ID do evento é obrigatório")]
        public int EventoId { get; set; }

        public bool Participou { get; set; }

    }

    public class ParticipacaoEventoUpdateDTO
    {
        public bool Participou { get; set; }

    }
}