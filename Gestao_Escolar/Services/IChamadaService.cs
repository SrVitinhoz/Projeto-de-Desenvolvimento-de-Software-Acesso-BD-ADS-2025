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
    }
}