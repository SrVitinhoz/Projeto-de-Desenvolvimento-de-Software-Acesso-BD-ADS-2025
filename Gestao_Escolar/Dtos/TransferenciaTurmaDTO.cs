using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de TransferenciaTurma
    public class TransferenciaTurmaCreateDTO
    {
        [Required]
        public int AlunoId { get; set; }
        
        [Required]
        public int TurmaOrigemId { get; set; }
        
        [Required]
        public int TurmaDestinoId { get; set; }
        
        [Required]
        public DateTime DataTransferencia { get; set; }
        
        public string Motivo { get; set; }
    }
    
    // DTO para atualização de TransferenciaTurma
    public class TransferenciaTurmaUpdateDTO
    {
        public string Motivo { get; set; }
    }
    
    // DTO para exibição de TransferenciaTurma
    public class TransferenciaTurmaDTO
    {
        public int Id { get; set; }
        
        public int AlunoId { get; set; }
        
        public string AlunoNome { get; set; }
        
        public int TurmaOrigemId { get; set; }
        
        public string TurmaOrigemNome { get; set; }
        
        public int TurmaDestinoId { get; set; }
        
        public string TurmaDestinoNome { get; set; }
        
        public DateTime DataTransferencia { get; set; }
        
        public string Motivo { get; set; }
    }
}