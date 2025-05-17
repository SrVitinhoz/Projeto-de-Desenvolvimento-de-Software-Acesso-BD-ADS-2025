using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do evento é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O valor em sonhos é obrigatório")]
        public int ValorSonhos { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O status do evento é obrigatório")]
        public string Status { get; set; } = null!;
    }

    public class EventoCreateDTO
    {
        [Required(ErrorMessage = "O nome do evento é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O valor em sonhos é obrigatório")]
        public int ValorSonhos { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O status do evento é obrigatório")]
        public string Status { get; set; } = null!;
    }

    public class EventoUpdateDTO
    {
        [Required(ErrorMessage = "O nome do evento é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O valor em sonhos é obrigatório")]
        public int ValorSonhos { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O status do evento é obrigatório")]
        public string Status { get; set; } = null!;
    }
}