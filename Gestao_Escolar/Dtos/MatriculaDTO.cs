using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Matricula
    public class MatriculaCreateDTO
    {
        [Required]
        public int AlunoId { get; set; }
        
        [Required]
        public int TurmaId { get; set; }
        
        [Required]
        public DateTime DataMatricula { get; set; }
        
        [Required]
        public string Status { get; set; } // "Ativa", "Cancelada", "Concluída"
        
        public string Observacao { get; set; }
    }
    
    // DTO para atualização de Matricula
    public class MatriculaUpdateDTO
    {
        public string Status { get; set; }
        
        public string Observacao { get; set; }
        
        public DateTime? DataConclusao { get; set; }
    }
    
    // DTO para exibição de Matricula
    public class MatriculaDTO
    {
        public int Id { get; set; }
        
        public int AlunoId { get; set; }
        
        public string AlunoNome { get; set; }
        
        public int TurmaId { get; set; }
        
        public string TurmaNome { get; set; }
        
        public DateTime DataMatricula { get; set; }
        
        public DateTime? DataConclusao { get; set; }
        
        public string Status { get; set; }
        
        public string Observacao { get; set; }
    }
}