using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEscolar.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        public int ValorSonhos { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataInicio { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }

        [Required]
        [Column(TypeName = "enum('ativo', 'encerrado')")]
        public string Status { get; set; }

        // Propriedades de navegação
        public virtual ICollection<ParticipacaoEvento> ParticipacaoEventos { get; set; }
    }
}
