using GestaoEscolar.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Escolar.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

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
            // Configurações específicas para cada entidade

            // Configuração para Funcionario (CPF único)
            modelBuilder.Entity<Funcionario>()
                .HasIndex(f => f.Cpf)
                .IsUnique();

            // Configuração para relacionamentos

            // Relacionamento Turma -> Aluno (1:N)
            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Funcionario -> Materia (N:1)
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Materia)
                .WithMany(m => m.Funcionarios)
                .HasForeignKey(f => f.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração para TransferenciaTurma (relacionamentos com Turma)
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

            // Configuração para valores padrão
            modelBuilder.Entity<Aluno>()
                .Property(a => a.SaldoSonhos)
                .HasDefaultValue(0);

            modelBuilder.Entity<ParticipacaoEvento>()
                .Property(p => p.Participou)
                .HasDefaultValue(false);
        }

    }
}
