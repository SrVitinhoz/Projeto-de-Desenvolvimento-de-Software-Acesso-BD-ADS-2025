using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Turma
    public class TurmaCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Periodo { get; set; }
        
        [Required]
        public int AnoLetivo { get; set; }
        
        public int? FuncionarioId { get; set; }
    }
    
    // DTO para atualização de Turma
    public class TurmaUpdateDTO
    {
        public string Nome { get; set; }
        
        public string Periodo { get; set; }
        
        public int? AnoLetivo { get; set; }
        
        public int? FuncionarioId { get; set; }
    }
    
    // DTO para exibição de Turma
    public class TurmaDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Periodo { get; set; }
        
        public int AnoLetivo { get; set; }
        
        public int? FuncionarioId { get; set; }
        
        public string FuncionarioNome { get; set; }
        
        public List<AlunoDTO> Alunos { get; set; }
        
        public List<MateriaDTO> Materias { get; set; }
        
        public List<ChamadaDTO> Chamadas { get; set; }
    }
}