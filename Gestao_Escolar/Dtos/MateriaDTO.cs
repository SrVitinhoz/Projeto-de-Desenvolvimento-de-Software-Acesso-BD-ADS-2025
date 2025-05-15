using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Materia
    public class MateriaCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public int CargaHoraria { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        [Required]
        public int TurmaId { get; set; }
    }
    
    // DTO para atualização de Materia
    public class MateriaUpdateDTO
    {
        public string Nome { get; set; }
        
        public string Descricao { get; set; }
        
        public int? CargaHoraria { get; set; }
        
        public int? FuncionarioId { get; set; }
    }
    
    // DTO para exibição de Materia
    public class MateriaDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Descricao { get; set; }
        
        public int CargaHoraria { get; set; }
        
        public int FuncionarioId { get; set; }
        
        public string FuncionarioNome { get; set; }
        
        public int TurmaId { get; set; }
        
        public string TurmaNome { get; set; }
        
        public List<ChamadaDTO> Chamadas { get; set; }
    }
}