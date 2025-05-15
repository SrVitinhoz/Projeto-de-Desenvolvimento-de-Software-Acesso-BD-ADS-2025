using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class ParticipacaoEvento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [Required]
        public int EventoId { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Participou { get; set; }

        [StringLength(255)]
        public string AlunoTurma { get; set; }

        [StringLength(255)]
        public string AlunoPeriodo { get; set; }

        // Propriedades de navegação
        [ForeignKey("AlunoId")]
        public virtual Aluno Aluno { get; set; }

        [ForeignKey("EventoId")]
        public virtual Evento Evento { get; set; }
    }
}
