using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestao_Escolar.DbContext;
using System.Linq.Expressions;

namespace Gestao_Escolar.Services
{
    public interface IChamadaService : IBaseService<Chamada, ChamadaDTO, ChamadaCreateDTO, ChamadaUpdateDTO>
    {
        Task<IEnumerable<ChamadaDTO>> GetChamadasByTurmaDataAsync(int turmaId, DateTime data);
        Task<ChamadaDTO?> RegistrarChamadaCompletaAsync(ChamadaCreateDTO chamadaDto, List<ChamadaAlunoCreateDTO> presencas);

        // NOVOS MÉTODOS
        Task<ChamadaListaAlunosDTO?> ConsultarAlunosParaChamadaAsync(ChamadaConsultaDTO consultaDto);
        Task<ChamadaDTO?> SalvarChamadaCompletaAsync(SalvarChamadaDTO salvarDto);
        Task<bool> VerificarExistenciaChamadaAsync(int turmaId, PeriodoTurma periodo, DateTime data);
    }

    public class ChamadaService : IChamadaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ChamadaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChamadaDTO>> GetAllAsync()
        {
            var chamadas = await _context.Chamadas
                .Include(c => c.Funcionario)
                .Include(c => c.Turma)
                .Include(c => c.Materia)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaDTO>>(chamadas);
        }

        public async Task<ChamadaDTO?> GetByIdAsync(int id)
        {
            var chamada = await _context.Chamadas
                .Include(c => c.Funcionario)
                .Include(c => c.Turma)
                .Include(c => c.Materia)
                .FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<ChamadaDTO>(chamada);
        }

        public async Task<ChamadaDTO> CreateAsync(ChamadaCreateDTO createDto)
        {
            var chamada = _mapper.Map<Chamada>(createDto);
            _context.Chamadas.Add(chamada);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChamadaDTO>(chamada);
        }

        public async Task<ChamadaDTO?> UpdateAsync(int id, ChamadaUpdateDTO updateDto)
        {
            var chamada = await _context.Chamadas.FindAsync(id);
            if (chamada == null) return null;

            _mapper.Map(updateDto, chamada);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChamadaDTO>(chamada);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chamada = await _context.Chamadas.FindAsync(id);
            if (chamada == null) return false;

            _context.Chamadas.Remove(chamada);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ChamadaDTO>> FindAsync(Expression<Func<Chamada, bool>> predicate)
        {
            var chamadas = await _context.Chamadas
                .Include(c => c.Funcionario)
                .Include(c => c.Turma)
                .Include(c => c.Materia)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaDTO>>(chamadas);
        }

        public async Task<IEnumerable<ChamadaDTO>> GetChamadasByTurmaDataAsync(int turmaId, DateTime data)
        {
            var chamadas = await _context.Chamadas
                .Include(c => c.Funcionario)
                .Include(c => c.Turma)
                .Include(c => c.Materia)
                .Where(c => c.TurmaId == turmaId && c.Data.Date == data.Date)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ChamadaDTO>>(chamadas);
        }

        public async Task<ChamadaDTO?> RegistrarChamadaCompletaAsync(ChamadaCreateDTO chamadaDto, List<ChamadaAlunoCreateDTO> presencas)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Criar a chamada
                var chamada = _mapper.Map<Chamada>(chamadaDto);
                _context.Chamadas.Add(chamada);
                await _context.SaveChangesAsync();

                // Registrar presenças
                foreach (var presenca in presencas)
                {
                    presenca.ChamadaId = chamada.Id;
                    var chamadaAluno = _mapper.Map<ChamadaAluno>(presenca);
                    _context.ChamadasAluno.Add(chamadaAluno);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return _mapper.Map<ChamadaDTO>(chamada);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ChamadaListaAlunosDTO?> ConsultarAlunosParaChamadaAsync(ChamadaConsultaDTO consultaDto)
        {
            // Buscar turma e alunos
            var turma = await _context.Turmas
                .Include(t => t.Alunos.Where(a => a.StatusMatricula == StatusMatricula.Ativo && a.Periodo == consultaDto.Periodo))
                .FirstOrDefaultAsync(t => t.Id == consultaDto.TurmaId && t.Periodo == consultaDto.Periodo);

            if (turma == null) return null;

            // Buscar funcionário
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.Id == consultaDto.FuncionarioId);

            if (funcionario == null) return null;

            // Verificar se já existe chamada
            var chamadaExistente = await _context.Chamadas
                .Include(c => c.ChamadasAluno)
                .ThenInclude(ca => ca.Aluno)
                .FirstOrDefaultAsync(c =>
                    c.TurmaId == consultaDto.TurmaId &&
                    c.Periodo == consultaDto.Periodo &&
                    c.Data.Date == consultaDto.Data.Date);

            var resultado = new ChamadaListaAlunosDTO
            {
                ChamadaId = chamadaExistente?.Id,
                TurmaId = turma.Id,
                TurmaNome = turma.Nome,
                Periodo = turma.Periodo,
                FuncionarioId = funcionario.Id,
                FuncionarioNome = funcionario.Nome, // Assumindo que Funcionario tem propriedade Nome
                Data = consultaDto.Data,
                JaExisteChamada = chamadaExistente != null
            };

            // Mapear alunos
            if (chamadaExistente != null)
            {
                // Chamada já existe - carregar dados existentes
                resultado.Alunos = chamadaExistente.ChamadasAluno.Select(ca => new AlunoPresencaDTO
                {
                    AlunoId = ca.AlunoId,
                    NomeAluno = ca.Aluno?.Nome ?? "",
                    Status = ca.Status,
                    Observacao = ca.Observacao,
                    ChamadaAlunoId = ca.Id
                }).ToList();
            }
            else
            {
                // Nova chamada - carregar alunos da turma
                resultado.Alunos = turma.Alunos
                    .Where(a => a.StatusMatricula == StatusMatricula.Ativo)
                    .Select(a => new AlunoPresencaDTO
                    {
                        AlunoId = a.Id,
                        NomeAluno = a.Nome,
                        Status = StatusPresenca.Presente, // Default
                        Observacao = null,
                        ChamadaAlunoId = null
                    }).ToList();
            }

            return resultado;
        }

        public async Task<ChamadaDTO?> SalvarChamadaCompletaAsync(SalvarChamadaDTO salvarDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Chamada chamada;

                if (salvarDto.ChamadaId.HasValue)
                {
                    // Atualizar chamada existente
                    chamada = await _context.Chamadas
                        .Include(c => c.ChamadasAluno)
                        .FirstOrDefaultAsync(c => c.Id == salvarDto.ChamadaId.Value);

                    if (chamada == null) return null;

                    // Atualizar dados da chamada
                    chamada.Data = salvarDto.Data;
                    chamada.Periodo = salvarDto.Periodo;
                    chamada.FuncionarioId = salvarDto.FuncionarioId;
                    chamada.MateriaId = salvarDto.MateriaId;

                    // Remover registros antigos de ChamadaAluno
                    _context.ChamadasAluno.RemoveRange(chamada.ChamadasAluno);
                }
                else
                {
                    // Criar nova chamada
                    chamada = new Chamada
                    {
                        TurmaId = salvarDto.TurmaId,
                        Periodo = salvarDto.Periodo,
                        FuncionarioId = salvarDto.FuncionarioId,
                        MateriaId = salvarDto.MateriaId,
                        Data = salvarDto.Data
                    };

                    _context.Chamadas.Add(chamada);
                }

                await _context.SaveChangesAsync();

                // Adicionar novos registros de ChamadaAluno
                foreach (var aluno in salvarDto.Alunos)
                {
                    var chamadaAluno = new ChamadaAluno
                    {
                        ChamadaId = chamada.Id,
                        AlunoId = aluno.AlunoId,
                        Status = aluno.Status,
                        Observacao = aluno.Observacao
                    };
                    _context.ChamadasAluno.Add(chamadaAluno);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Retornar DTO da chamada
                var chamadaCompleta = await _context.Chamadas
                    .Include(c => c.Funcionario)
                    .Include(c => c.Turma)
                    .Include(c => c.Materia)
                    .FirstOrDefaultAsync(c => c.Id == chamada.Id);

                return _mapper.Map<ChamadaDTO>(chamadaCompleta);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> VerificarExistenciaChamadaAsync(int turmaId, PeriodoTurma periodo, DateTime data)
        {
            return await _context.Chamadas
                .AnyAsync(c =>
                    c.TurmaId == turmaId &&
                    c.Periodo == periodo &&
                    c.Data.Date == data.Date);
        }
    }
}