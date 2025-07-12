using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestao_Escolar.Models;


    namespace Gestao_Escolar.Models
    {
        public class Funcionario
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Nome { get; set; } = null!;

            [Required]
            [StringLength(14)]
            public string Cpf { get; set; } = null!;

            [Required]
            public CargoFuncionario Cargo { get; set; }

            [Required]
            public StatusFuncionario Status { get; set; }

            [Required]
            public DateTime DataAdimissao { get; set; }

            public DateTime? DataDesligamento { get; set; }

            [Required]
            [StringLength(100)]
            public string Senha { get; set; } = null!;

            public int? MateriaId { get; set; }

            [ForeignKey("MateriaId")]
            public virtual Materia? Materia { get; set; }


            public virtual ICollection<Chamada>? Chamadas { get; set; }
            public virtual ICollection<HistoricoSonhos>? HistoricoSonhos { get; set; }
            public virtual ICollection<TransferenciaTurma>? TransferenciasTurma { get; set; }
        }

        public enum CargoFuncionario
        {
            Professor,
            Administrativo
        }

        public enum StatusFuncionario
        {
            Ativo,
            Desligado
        }
    }
