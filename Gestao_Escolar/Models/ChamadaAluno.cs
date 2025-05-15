using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class ChamadaAluno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChamadaId { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [Required]
        [Column(TypeName = "enum('presente', 'ausente', 'justificado')")]
        public string Status { get; set; }

        [StringLength(255)]
        public string Observacao { get; set; }

        // Propriedades de navegação
        [ForeignKey("ChamadaId")]
        public virtual Chamada Chamada { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno Aluno { get; set; }
    }
}
