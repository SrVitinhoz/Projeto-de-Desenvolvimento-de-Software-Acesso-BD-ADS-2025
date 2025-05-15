using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de ParticipacaoEvento
    public class ParticipacaoEventoCreateDTO
    {
        [Required]
        public int EventoId { get; set; }
        
        [Required]
        public int AlunoId { get; set; }
        
        [Required]
        public bool Confirmado { get; set; }
        
        public string Observacao { get; set; }
    }
    
    // DTO para atualização de ParticipacaoEvento
    public class ParticipacaoEventoUpdateDTO
    {
        public bool? Confirmado { get; set; }
        
        public bool? Presente { get; set; }
        
        public string Observacao { get; set; }
        
        public bool? SonhosRecebidos { get; set; }
    }
    
    // DTO para exibição de ParticipacaoEvento
    public class ParticipacaoEventoDTO
    {
        public int Id { get; set; }
        
        public int EventoId { get; set; }
        
        public string EventoNome { get; set; }
        
        public int AlunoId { get; set; }
        
        public string AlunoNome { get; set; }
        
        public bool Confirmado { get; set; }
        
        public bool Presente { get; set; }
        
        public string Observacao { get; set; }
        
        public bool SonhosRecebidos { get; set; }
        
        public DateTime DataEvento { get; set; }
    }
}