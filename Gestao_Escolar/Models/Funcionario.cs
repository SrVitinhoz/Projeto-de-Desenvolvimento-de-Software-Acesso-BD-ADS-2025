using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [Column(TypeName = "enum('professor', 'administrativo')")]
        public string Cargo { get; set; }

        [Required]
        [Column(TypeName = "enum('ativo', 'desligado')")]
        public string Status { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataAdimicao { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DataDesligamento { get; set; }

        public int? MateriaId { get; set; }

        // Propriedades de navegação
        [ForeignKey("MateriaId")]
        public virtual Materia Materia { get; set; }
        public virtual ICollection<Chamada> Chamadas { get; set; }
        public virtual ICollection<HistoricoSonhos> HistoricoSonhos { get; set; }
        public virtual ICollection<TransferenciaTurma> Transferencias { get; set; }
        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}
