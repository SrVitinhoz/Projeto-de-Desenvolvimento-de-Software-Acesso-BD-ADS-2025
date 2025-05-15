using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de HistoricoSonhos
    public class HistoricoSonhosCreateDTO
    {
        [Required]
        public int AlunoId { get; set; }
        
        [Required]
        public int Quantidade { get; set; }
        
        [Required]
        public string Tipo { get; set; } // "Crédito" ou "Débito"
        
        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public DateTime Data { get; set; }
    }
    
    // DTO para atualização de HistoricoSonhos
    public class HistoricoSonhosUpdateDTO
    {
        public string Descricao { get; set; }
    }
    
    // DTO para exibição de HistoricoSonhos
    public class HistoricoSonhosDTO
    {
        public int Id { get; set; }
        
        public int AlunoId { get; set; }
        
        public string AlunoNome { get; set; }
        
        public int Quantidade { get; set; }
        
        public string Tipo { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime Data { get; set; }
        
        public int SaldoAposOperacao { get; set; }
    }
}