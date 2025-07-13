using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gestao_Escolar.DbContext;

namespace Gestao_Escolar.Services
{
    public interface IParticipacaoEventoService : IBaseService<ParticipacaoEvento, ParticipacaoEventoDTO, ParticipacaoEventoCreateDTO, ParticipacaoEventoUpdateDTO>
    {
        Task<bool> CancelarParticipacaoAsync(int participacaoId);
        Task<IEnumerable<ParticipacaoEventoDTO>> GetByEventoIdAsync(int eventoId);
        Task<IEnumerable<ParticipacaoEventoDTO>> GetByAlunoIdAsync(int alunoId);
        Task<bool> RegistrarParticipacaoAsync(int eventoId, int alunoId);

    }

    public class ParticipacaoEventoService : IParticipacaoEventoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHistoricoSonhosService _historicoSonhosService;

        public ParticipacaoEventoService(
            AppDbContext context,
            IMapper mapper,
            IHistoricoSonhosService historicoSonhosService)
        {
            _context = context;
            _mapper = mapper;
            _historicoSonhosService = historicoSonhosService;
        }

        public async Task<IEnumerable<ParticipacaoEventoDTO>> GetAllAsync()
        {
            var participacoes = await _context.ParticipacaoEventos
                .Include(p => p.Aluno)
                .Include(p => p.Evento)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ParticipacaoEventoDTO>>(participacoes);
        }

        public async Task<ParticipacaoEventoDTO?> GetByIdAsync(int id)
        {
            var participacao = await _context.ParticipacaoEventos
                .Include(p => p.Aluno)
                .Include(p => p.Evento)
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ParticipacaoEventoDTO>(participacao);
        }

        public async Task<ParticipacaoEventoDTO> CreateAsync(ParticipacaoEventoCreateDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                var evento = await _context.Eventos.FindAsync(createDto.EventoId);
                var aluno = await _context.Alunos.FindAsync(createDto.AlunoId);

                if (evento == null || aluno == null)
                {
                    throw new InvalidOperationException("Evento ou aluno não encontrado.");
                }


                if (aluno.SaldoSonhos < evento.ValorSonhos)
                {
                    throw new InvalidOperationException($"Saldo insuficiente. Necessário: {evento.ValorSonhos} sonhos. Disponível: {aluno.SaldoSonhos} sonhos.");
                }


                var participacaoExistente = await _context.ParticipacaoEventos
                    .FirstOrDefaultAsync(p => p.EventoId == createDto.EventoId && p.AlunoId == createDto.AlunoId);

                if (participacaoExistente != null)
                {
                    throw new InvalidOperationException("Aluno já está inscrito neste evento.");
                }


                var participacao = _mapper.Map<ParticipacaoEvento>(createDto);
                _context.ParticipacaoEventos.Add(participacao);


                await _historicoSonhosService.SubtrairSonhosAsync(
                    createDto.AlunoId,
                    evento.ValorSonhos,
                    $"Inscrição no evento: {evento.Nome}",
                    null);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


                var participacaoCompleta = await _context.ParticipacaoEventos
                    .Include(p => p.Aluno)
                    .Include(p => p.Evento)
                    .FirstOrDefaultAsync(p => p.Id == participacao.Id);

                return _mapper.Map<ParticipacaoEventoDTO>(participacaoCompleta);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ParticipacaoEventoDTO?> UpdateAsync(int id, ParticipacaoEventoUpdateDTO updateDto)
        {
            var participacao = await _context.ParticipacaoEventos.FindAsync(id);
            if (participacao == null) return null;

            _mapper.Map(updateDto, participacao);
            await _context.SaveChangesAsync();
            return _mapper.Map<ParticipacaoEventoDTO>(participacao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var participacao = await _context.ParticipacaoEventos.FindAsync(id);
            if (participacao == null) return false;

            _context.ParticipacaoEventos.Remove(participacao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ParticipacaoEventoDTO>> FindAsync(Expression<Func<ParticipacaoEvento, bool>> predicate)
        {
            var participacoes = await _context.ParticipacaoEventos
                .Include(p => p.Aluno)
                .Include(p => p.Evento)
                .Where(predicate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ParticipacaoEventoDTO>>(participacoes);
        }

        public async Task<IEnumerable<ParticipacaoEventoDTO>> GetByEventoIdAsync(int eventoId)
        {
            var participacoes = await _context.ParticipacaoEventos
                .Include(p => p.Aluno)
                .Where(p => p.EventoId == eventoId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ParticipacaoEventoDTO>>(participacoes);
        }

        public async Task<IEnumerable<ParticipacaoEventoDTO>> GetByAlunoIdAsync(int alunoId)
        {
            var participacoes = await _context.ParticipacaoEventos
                .Include(p => p.Evento)
                .Where(p => p.AlunoId == alunoId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ParticipacaoEventoDTO>>(participacoes);
        }

        public async Task<bool> RegistrarParticipacaoAsync(int eventoId, int alunoId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var evento = await _context.Eventos.FindAsync(eventoId);
                var aluno = await _context.Alunos.FindAsync(alunoId);

                if (evento == null || aluno == null) return false;

                if (aluno.SaldoSonhos < evento.ValorSonhos) return false;


                var participacaoExistente = await _context.ParticipacaoEventos
                    .FirstOrDefaultAsync(p => p.EventoId == eventoId && p.AlunoId == alunoId);

                if (participacaoExistente != null)
                {

                    participacaoExistente.Participou = true;
                }
                else
                {

                    var participacao = new ParticipacaoEvento
                    {
                        EventoId = eventoId,
                        AlunoId = alunoId,
                        Participou = true
                    };
                    _context.ParticipacaoEventos.Add(participacao);


                    var resultado = await _historicoSonhosService.SubtrairSonhosAsync(
                        alunoId,
                        evento.ValorSonhos,
                        $"Participação no evento: {evento.Nome}",
                        null);


                    if (resultado == null)
                    {
                        await transaction.RollbackAsync();
                        return false; 
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }


        public async Task<bool> CancelarParticipacaoAsync(int participacaoId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var participacao = await _context.ParticipacaoEventos
                    .Include(p => p.Evento)
                    .Include(p => p.Aluno)
                    .FirstOrDefaultAsync(p => p.Id == participacaoId);

                if (participacao == null) return false;


                await _historicoSonhosService.AdicionarSonhosAsync(
                    participacao.AlunoId,
                    participacao.Evento!.ValorSonhos,
                    $"Cancelamento da participação no evento: {participacao.Evento.Nome}",
                    null);


                _context.ParticipacaoEventos.Remove(participacao);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}