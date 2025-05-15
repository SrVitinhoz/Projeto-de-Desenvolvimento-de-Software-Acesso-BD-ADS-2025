using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "enum('ativo', 'desligado')")]
        public string Status { get; set; }

        [Required]
        [Column(TypeName = "enum('vespertino', 'matutino')")]
        public string Periodo { get; set; }

        // Propriedades de navegação
        public virtual ICollection<Aluno> Alunos { get; set; }
        public virtual ICollection<Chamada> Chamadas { get; set; }
        public virtual ICollection<TransferenciaTurma> TransferenciasOrigem { get; set; }
        public virtual ICollection<TransferenciaTurma> TransferenciasDestino { get; set; }
    }
}
