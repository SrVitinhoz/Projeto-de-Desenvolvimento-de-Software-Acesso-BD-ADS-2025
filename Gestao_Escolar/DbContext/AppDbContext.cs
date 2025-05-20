using Microsoft.EntityFrameworkCore;
using System;
using Gestao_Escolar.Models;

namespace Gestao_Escolar.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<ParticipacaoEvento> ParticipacaoEventos { get; set; }
        public DbSet<Chamada> Chamadas { get; set; }
        public DbSet<ChamadaAluno> ChamadasAluno { get; set; }
        public DbSet<HistoricoSonhos> HistoricoSonhos { get; set; }
        public DbSet<TransferenciaTurma> TransferenciasTurma { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar nomes das tabelas para corresponder ao banco de dados
            modelBuilder.Entity<Turma>().ToTable("Turma");
            modelBuilder.Entity<Materia>().ToTable("Materia");
            modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
            modelBuilder.Entity<Aluno>().ToTable("Aluno");
            modelBuilder.Entity<Evento>().ToTable("Evento");
            modelBuilder.Entity<ParticipacaoEvento>().ToTable("ParticipacaoEvento");
            modelBuilder.Entity<Chamada>().ToTable("Chamada");
            modelBuilder.Entity<ChamadaAluno>().ToTable("ChamadaAluno");
            modelBuilder.Entity<HistoricoSonhos>().ToTable("HistoricoSonhos");
            modelBuilder.Entity<TransferenciaTurma>().ToTable("TransferenciaTurma");
            modelBuilder.Entity<Matricula>().ToTable("Matricula");

            // Configuração de relacionamentos e restrições

            // Configuração para Funcionario
            modelBuilder.Entity<Funcionario>()
                .HasIndex(f => f.Cpf)
                .IsUnique();

            // Configuração para Aluno
            modelBuilder.Entity<Aluno>()
                .Property(a => a.SaldoSonhos)
                .HasDefaultValue(0);

            // Configuração para ParticipacaoEvento
            modelBuilder.Entity<ParticipacaoEvento>()
                .Property(p => p.Participou)
                .HasDefaultValue(false);

            // Configuração para TransferenciaTurma (relacionamentos múltiplos com Turma)
            modelBuilder.Entity<TransferenciaTurma>()
                .HasOne(t => t.TurmaOrigem)
                .WithMany(t => t.TransferenciasOrigem)
                .HasForeignKey(t => t.TurmaOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferenciaTurma>()
                .HasOne(t => t.TurmaDestino)
                .WithMany(t => t.TransferenciasDestino)
                .HasForeignKey(t => t.TurmaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração de conversão de enums para strings no banco
            modelBuilder.Entity<Turma>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Turma>()
                .Property(t => t.Periodo)
                .HasConversion<string>();

            modelBuilder.Entity<Materia>()
                .Property(m => m.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Funcionario>()
                .Property(f => f.Cargo)
                .HasConversion<string>();

            modelBuilder.Entity<Funcionario>()
                .Property(f => f.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Aluno>()
                .Property(a => a.StatusMatricula)
                .HasConversion<string>();

            modelBuilder.Entity<Aluno>()
                .Property(a => a.Periodo)
                .HasConversion<string>();

            modelBuilder.Entity<Evento>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Chamada>()
                .Property(c => c.Periodo)
                .HasConversion<string>();

            modelBuilder.Entity<ChamadaAluno>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<HistoricoSonhos>()
                .Property(h => h.Tipo)
                .HasConversion<string>();

            modelBuilder.Entity<Matricula>()
                .Property(m => m.Status)
                .HasConversion<string>();
        }
        
    }
}