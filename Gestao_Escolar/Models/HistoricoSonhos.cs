using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestao_Escolar.Models;

namespace Gestao_Escolar.Models
{
    public class HistoricoSonhos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AlunoId { get; set; }

        [ForeignKey("AlunoId")]
        public virtual Aluno? Aluno { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public TipoOperacaoSonhos Tipo { get; set; }

        [StringLength(255)]
        public string? Motivo { get; set; }

        [Required]
        public int Valor { get; set; }

        public int? FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario? Funcionario { get; set; }
    }

    public enum TipoOperacaoSonhos
    {
        Adição,
        Subtração,
        UsoEvento
    }
}