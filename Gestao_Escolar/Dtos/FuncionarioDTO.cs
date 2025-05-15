using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Funcionario
    public class FuncionarioCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string CPF { get; set; }
        
        [Required]
        public DateTime DataNascimento { get; set; }
        
        [Required]
        public string Endereco { get; set; }
        
        [Required]
        public string Telefone { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Cargo { get; set; }
        
        [Required]
        public DateTime DataContratacao { get; set; }
        
        public string Formacao { get; set; }
        
        public string Especializacao { get; set; }
    }
    
    // DTO para atualização de Funcionario
    public class FuncionarioUpdateDTO
    {
        public string Nome { get; set; }
        
        public string Endereco { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }
        
        public string Cargo { get; set; }
        
        public string Formacao { get; set; }
        
        public string Especializacao { get; set; }
    }
    
    // DTO para exibição de Funcionario
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string CPF { get; set; }
        
        public DateTime DataNascimento { get; set; }
        
        public string Endereco { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }
        
        public string Cargo { get; set; }
        
        public DateTime DataContratacao { get; set; }
        
        public string Formacao { get; set; }
        
        public string Especializacao { get; set; }
        
        public List<TurmaDTO> Turmas { get; set; }
        
        public List<MateriaDTO> Materias { get; set; }
        
        public List<EventoDTO> EventosCriados { get; set; }
    }
}