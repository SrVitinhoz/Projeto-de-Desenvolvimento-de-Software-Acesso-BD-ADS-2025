using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Evento
    public class EventoCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public DateTime DataInicio { get; set; }
        
        [Required]
        public DateTime DataFim { get; set; }
        
        [Required]
        public string Local { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        public int? SonhosRecompensa { get; set; }
    }
    
    // DTO para atualização de Evento
    public class EventoUpdateDTO
    {
        public string Nome { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime? DataInicio { get; set; }
        
        public DateTime? DataFim { get; set; }
        
        public string Local { get; set; }
        
        public int? SonhosRecompensa { get; set; }
    }
    
    // DTO para exibição de Evento
    public class EventoDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime DataInicio { get; set; }
        
        public DateTime DataFim { get; set; }
        
        public string Local { get; set; }
        
        public int FuncionarioId { get; set; }
        
        public string FuncionarioNome { get; set; }
        
        public int? SonhosRecompensa { get; set; }
        
        public List<ParticipacaoEventoDTO> Participacoes { get; set; }
    }
}