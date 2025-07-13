using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_Escolar.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        public StatusTurma Status { get; set; }

        [Required]
        public PeriodoTurma Periodo { get; set; }


        public virtual ICollection<Aluno>? Alunos { get; set; }
        public virtual ICollection<Chamada>? Chamadas { get; set; }
        public virtual ICollection<TransferenciaTurma>? TransferenciasOrigem { get; set; }
        public virtual ICollection<TransferenciaTurma>? TransferenciasDestino { get; set; }
    }

    public enum StatusTurma
    {
        Ativo,
        Desligado
    }

    public enum PeriodoTurma
    {
        Matutino,
        Vespertino
    }
}