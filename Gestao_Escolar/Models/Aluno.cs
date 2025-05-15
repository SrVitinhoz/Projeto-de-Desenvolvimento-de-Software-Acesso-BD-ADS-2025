using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataNascimento { get; set; }

        public byte[] FotoUrl { get; set; }

        public int? TurmaId { get; set; }

        [Required]
        [Column(TypeName = "enum('ativo', 'desligado', 'inativa')")]
        public string StatusMatricula { get; set; }

        [Required]
        [DefaultValue(0)]
        public int SaldoSonhos { get; set; }

        [Required]
        public int NumMatricula { get; set; }

        [Required]
        [Column(TypeName = "enum('matutino', 'vespertino')")]
        public string Periodo { get; set; }

        // Propriedades de navegação
        [ForeignKey("TurmaId")]
        public virtual Turma Turma { get; set; }
        public virtual ICollection<ParticipacaoEvento> ParticipacaoEventos { get; set; }
        public virtual ICollection<ChamadaAluno> ChamadasAluno { get; set; }
        public virtual ICollection<HistoricoSonhos> HistoricoSonhos { get; set; }
        public virtual ICollection<TransferenciaTurma> Transferencias { get; set; }
        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}
