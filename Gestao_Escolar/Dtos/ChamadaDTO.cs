using Gestao_Escolar.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class ChamadaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do funcionário é obrigatório")]
        public int FuncionarioId { get; set; }

        public string? FuncionarioNome { get; set; }

        [Required(ErrorMessage = "O ID da turma é obrigatório")]
        public int TurmaId { get; set; }

        public string? TurmaNome { get; set; }

        [Required(ErrorMessage = "O ID da matéria é obrigatório")]
        public int MateriaId { get; set; }

        public string? MateriaNome { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O período é obrigatório")]
        public string Periodo { get; set; } = null!;
    }

    public class ChamadaCreateDTO
    {
        [Required(ErrorMessage = "O ID do funcionário é obrigatório")]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "O ID da turma é obrigatório")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "O ID da matéria é obrigatório")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O período é obrigatório")]
        public string Periodo { get; set; } = null!;
    }

    public class ChamadaUpdateDTO
    {
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O período é obrigatório")]
        public string Periodo { get; set; } = null!;
    }


    public class ChamadaConsultaDTO
    {
        [Required(ErrorMessage = "O ID da turma é obrigatório")]
        public int TurmaId { get; set; }

        [Required(ErrorMessage = "O período é obrigatório")]
        public PeriodoTurma Periodo { get; set; }

        [Required(ErrorMessage = "O ID do funcionário é obrigatório")]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }
    }

    public class AlunoPresencaDTO
    {
        public int AlunoId { get; set; }
        public string NomeAluno { get; set; } = null!;
        public StatusPresenca Status { get; set; }
        public string? Observacao { get; set; }
        public int? ChamadaAlunoId { get; set; }
    }

    public class ChamadaListaAlunosDTO
    {
        public int? ChamadaId { get; set; }
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; } = null!;
        public PeriodoTurma Periodo { get; set; }
        public int FuncionarioId { get; set; }
        public string FuncionarioNome { get; set; } = null!;
        public DateTime Data { get; set; }
        public List<AlunoPresencaDTO> Alunos { get; set; } = new();
        public bool JaExisteChamada { get; set; }
    }

    public class SalvarChamadaDTO
    {
        public int? ChamadaId { get; set; }
        public int TurmaId { get; set; }
        public PeriodoTurma Periodo { get; set; }
        public int FuncionarioId { get; set; }
        public int MateriaId { get; set; }
        public DateTime Data { get; set; }
        public List<AlunoPresencaDTO> Alunos { get; set; } = new();
    }
}