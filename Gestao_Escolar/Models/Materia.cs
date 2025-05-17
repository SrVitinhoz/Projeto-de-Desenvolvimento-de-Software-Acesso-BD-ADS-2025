using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_Escolar.Models
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        public StatusMateria Status { get; set; }

        // Propriedades de navegação
        public virtual ICollection<Funcionario>? Funcionarios { get; set; }
        public virtual ICollection<Chamada>? Chamadas { get; set; }
    }

    public enum StatusMateria
    {
        Ativo,
        Desligado
    }
}