using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_Escolar.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        [Required]
        public int ValorSonhos { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        [Required]
        public StatusEvento Status { get; set; }

        public virtual ICollection<ParticipacaoEvento>? ParticipacaoEventos { get; set; }
    }

    public enum StatusEvento
    {
        Ativo,
        Encerrado
    }
}