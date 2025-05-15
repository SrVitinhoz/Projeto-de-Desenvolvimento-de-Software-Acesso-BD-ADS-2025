using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoEscolar.DTOs
{
    // DTO para criação de Aluno
    public class AlunoCreateDTO
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
        
        public string NomeResponsavel { get; set; }
        
        public string TelefoneResponsavel { get; set; }
        
        public int? TurmaId { get; set; }
        
        public int SonhosMoeda { get; set; } = 0;
    }
    
    // DTO para atualização de Aluno
    public class AlunoUpdateDTO
    {
        public string Nome { get; set; }
        
        public string Endereco { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }
        
        public string NomeResponsavel { get; set; }
        
        public string TelefoneResponsavel { get; set; }
        
        public int? TurmaId { get; set; }
        
        public int? SonhosMoeda { get; set; }
    }
    
    // DTO para exibição de Aluno
    public class AlunoDTO
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string CPF { get; set; }
        
        public DateTime DataNascimento { get; set; }
        
        public string Endereco { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }
        
        public string NomeResponsavel { get; set; }
        
        public string TelefoneResponsavel { get; set; }
        
        public int? TurmaId { get; set; }
        
        public string TurmaNome { get; set; }
        
        public int SonhosMoeda { get; set; }
        
        public List<ChamadaAlunoDTO> Chamadas { get; set; }
        
        public List<ParticipacaoEventoDTO> Participacoes { get; set; }
        
        public List<HistoricoSonhosDTO> HistoricoSonhos { get; set; }
        
        public List<TransferenciaTurmaDTO> Transferencias { get; set; }
    }
}