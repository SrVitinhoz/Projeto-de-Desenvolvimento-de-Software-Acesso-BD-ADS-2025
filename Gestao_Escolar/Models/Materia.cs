using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "enum('ativo', 'desligado')")]
        public string Status { get; set; }

        // Propriedades de navegação
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<Chamada> Chamadas { get; set; }
    }
}
