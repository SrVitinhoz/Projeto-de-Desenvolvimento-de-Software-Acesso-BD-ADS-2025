using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class TurmaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O status da turma é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "O período da turma é obrigatório")]
        public string Periodo { get; set; } = null!;
    }

    public class TurmaCreateDTO
    {
        [Required(ErrorMessage = "O nome da turma é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O status da turma é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "O período da turma é obrigatório")]
        public string Periodo { get; set; } = null!;
    }

    public class TurmaUpdateDTO
    {
        [Required(ErrorMessage = "O nome da turma é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O status da turma é obrigatório")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "O período da turma é obrigatório")]
        public string Periodo { get; set; } = null!;
    }
}