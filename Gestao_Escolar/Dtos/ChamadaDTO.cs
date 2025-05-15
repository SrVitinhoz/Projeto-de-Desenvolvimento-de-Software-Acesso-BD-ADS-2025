using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Chamada
    public class ChamadaCreateDTO
    {
        [Required]
        public DateTime Data { get; set; }
        
        [Required]
        public int MateriaId { get; set; }
        
        [Required]
        public int TurmaId { get; set; }
        
        [Required]
        public List<ChamadaAlunoCreateDTO> PresencasAlunos { get; set; }
    }
    
    // DTO para atualização de Chamada
    public class ChamadaUpdateDTO
    {
        public DateTime? Data { get; set; }
        
        public List<ChamadaAlunoUpdateDTO> PresencasAlunos { get; set; }
    }
    
    // DTO para exibição de Chamada
    public class ChamadaDTO
    {
        public int Id { get; set; }
        
        public DateTime Data { get; set; }
        
        public int MateriaId { get; set; }
        
        public string MateriaNome { get; set; }
        
        public int TurmaId { get; set; }
        
        public string TurmaNome { get; set; }
        
        public List<ChamadaAlunoDTO> PresencasAlunos { get; set; }
    }
}