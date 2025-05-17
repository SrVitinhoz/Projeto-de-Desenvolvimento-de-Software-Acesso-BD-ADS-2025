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
}