using Gestao_Escolar.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_Escolar.Models
{
    public class Chamada
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario? Funcionario { get; set; }

        [Required]
        public int TurmaId { get; set; }

        [ForeignKey("TurmaId")]
        public virtual Turma? Turma { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [ForeignKey("MateriaId")]
        public virtual Materia? Materia { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public PeriodoTurma Periodo { get; set; }

        public virtual ICollection<ChamadaAluno>? ChamadasAluno { get; set; }
    }
}