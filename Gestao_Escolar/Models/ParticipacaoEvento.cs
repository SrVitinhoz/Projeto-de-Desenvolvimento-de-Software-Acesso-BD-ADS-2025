using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestao_Escolar.Models;

namespace Gestao_Escolar.Models
{
    public class ParticipacaoEvento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno? Aluno { get; set; }

        [Required]
        public int EventoId { get; set; }

        [ForeignKey("EventoId")]
        public virtual Evento? Evento { get; set; }

        public bool Participou { get; set; } = false;

        [StringLength(255)]
        public string? AlunoTurma { get; set; }

        [StringLength(255)]
        public string? AlunoPeriodo { get; set; }
    }
}