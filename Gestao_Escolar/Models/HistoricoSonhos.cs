using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class HistoricoSonhos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }

        [Required]
        [Column(TypeName = "enum('adição', 'subtração', 'uso_evento')")]
        public string Tipo { get; set; }

        [StringLength(255)]
        public string Motivo { get; set; }

        [Required]
        public int Valor { get; set; }

        public int? FuncionarioId { get; set; }

        // Propriedades de navegação
        [ForeignKey("AlunoId")]
        public virtual Aluno Aluno { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }
    }
}
