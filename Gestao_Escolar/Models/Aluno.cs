using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestao_Escolar.Models;

namespace Gestao_Escolar.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        public DateTime DataNascimento { get; set; }


        public int? TurmaId { get; set; }

        [ForeignKey("TurmaId")]
        public virtual Turma? Turma { get; set; }

        [Required]
        public StatusMatricula StatusMatricula { get; set; }

        [Required]
        public int SaldoSonhos { get; set; } = 0;

        [Required]
        public int NumMatricula { get; set; }

        [Required]
        public PeriodoTurma Periodo { get; set; }

        //[json ignore]

        // Propriedades de navegação
        public virtual ICollection<ParticipacaoEvento>? ParticipacaoEventos { get; set; }
        public virtual ICollection<ChamadaAluno>? ChamadasAluno { get; set; }
        public virtual ICollection<HistoricoSonhos>? HistoricoSonhos { get; set; }
        public virtual ICollection<TransferenciaTurma>? TransferenciasTurma { get; set; }
    }

    public enum StatusMatricula
    {
        Ativo,
        Desligado,
        Inativo
    }
}