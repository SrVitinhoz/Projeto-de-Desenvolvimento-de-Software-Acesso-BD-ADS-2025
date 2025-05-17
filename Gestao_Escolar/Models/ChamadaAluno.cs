using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestao_Escolar.Models;

namespace Gestao_Escolar.Models
{
    public class ChamadaAluno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChamadaId { get; set; }

        [ForeignKey("ChamadaId")]
        public virtual Chamada? Chamada { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno? Aluno { get; set; }

        [Required]
        public StatusPresenca Status { get; set; }

        [StringLength(255)]
        public string? Observacao { get; set; }
    }

    public enum StatusPresenca
    {
        Presente,
        Ausente,
        Justificado
    }
}