using System.ComponentModel.DataAnnotations;

namespace Gestao_Escolar.DTOs
{
    public class MateriaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da matéria é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O status da matéria é obrigatório")]
        public string Status { get; set; } = null!;
    }

    public class MateriaCreateDTO
    {
        [Required(ErrorMessage = "O nome da matéria é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O status da matéria é obrigatório")]
        public string Status { get; set; } = null!;
    }

    public class MateriaUpdateDTO
    {
        [Required(ErrorMessage = "O nome da matéria é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O status da matéria é obrigatório")]
        public string Status { get; set; } = null!;
    }
}