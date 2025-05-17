using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestao_Escolar.Models;

namespace Gestao_Escolar.Models
{
    public class TransferenciaTurma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno? Aluno { get; set; }

        public int? TurmaOrigemId { get; set; }

        [ForeignKey("TurmaOrigemId")]
        public virtual Turma? TurmaOrigem { get; set; }

        public int? TurmaDestinoId { get; set; }

        [ForeignKey("TurmaDestinoId")]
        public virtual Turma? TurmaDestino { get; set; }

        [Required]
        public DateTime DataTransferencia { get; set; }

        public int? FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario? Funcionario { get; set; }
    }
}