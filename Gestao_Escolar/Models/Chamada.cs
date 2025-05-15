using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Chamada
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        [Required]
        public int TurmaId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        [Required]
        [Column(TypeName = "enum('matutino', 'vespertino')")]
        public string Periodo { get; set; }

        // Propriedades de navegação
        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }

        [ForeignKey("TurmaId")]
        public virtual Turma Turma { get; set; }

        [ForeignKey("MateriaId")]
        public virtual Materia Materia { get; set; }

        public virtual ICollection<ChamadaAluno> ChamadasAluno { get; set; }
    }
}
