using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de ChamadaAluno
    public class ChamadaAlunoCreateDTO
    {
        [Required]
        public int AlunoId { get; set; }
        
        [Required]
        public bool Presente { get; set; }
        
        public string Observacao { get; set; }
    }
    
    // DTO para atualização de ChamadaAluno
    public class ChamadaAlunoUpdateDTO
    {
        [Required]
        public int AlunoId { get; set; }
        
        public bool? Presente { get; set; }
        
        public string Observacao { get; set; }
    }
    
    // DTO para exibição de ChamadaAluno
    public class ChamadaAlunoDTO
    {
        public int Id { get; set; }
        
        public int ChamadaId { get; set; }
        
        public int AlunoId { get; set; }
        
        public string AlunoNome { get; set; }
        
        public bool Presente { get; set; }
        
        public string Observacao { get; set; }
        
        public DateTime DataChamada { get; set; }
    }
}