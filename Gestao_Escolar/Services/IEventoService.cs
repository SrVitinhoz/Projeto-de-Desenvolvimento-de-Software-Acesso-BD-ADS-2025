using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using Gestao_Escolar.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestao_Escolar.DbContext;
using System.Linq.Expressions;

namespace Gestao_Escolar.Services
{
    public interface IEventoService : IBaseService<Evento, EventoDTO, EventoCreateDTO, EventoUpdateDTO>
    {
        Task<IEnumerable<EventoDTO>> GetEventosAtivosAsync();
        Task<bool> RegistrarParticipacaoAsync(int eventoId, int alunoId);
    }

    public class EventoService : IEventoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventoDTO>> GetAllAsync()
        {
            var eventos = await _context.Eventos.ToListAsync();
            return _mapper.Map<IEnumerable<EventoDTO>>(eventos);
        }

        public async Task<EventoDTO?> GetByIdAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            return _mapper.Map<EventoDTO>(evento);
        }

        public async Task<EventoDTO> CreateAsync(EventoCreateDTO createDto)
        {
            var evento = _mapper.Map<Evento>(createDto);
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventoDTO>(evento);
        }

        public async Task<EventoDTO?> UpdateAsync(int id, EventoUpdateDTO updateDto)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return null;

            _mapper.Map(updateDto, evento);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventoDTO>(evento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EventoDTO>> FindAsync(Expression<Func<Evento, bool>> predicate)
        {
            var eventos = await _context.Eventos.Where(predicate).ToListAsync();
            return _mapper.Map<IEnumerable<EventoDTO>>(eventos);
        }

        public async Task<IEnumerable<EventoDTO>> GetEventosAtivosAsync()
        {
            var eventos = await _context.Eventos
                .Where(e => e.Status == StatusEvento.Ativo)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EventoDTO>>(eventos);
        }

        public async Task<bool> RegistrarParticipacaoAsync(int eventoId, int alunoId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var evento = await _context.Eventos.FindAsync(eventoId);
                var aluno = await _context.Alunos.FindAsync(alunoId);

                if (evento == null || aluno == null) return false;

                var participacao = new ParticipacaoEvento
                {
                    EventoId = eventoId,
                    AlunoId = alunoId,
                    Participou = true
                };

                _context.ParticipacaoEventos.Add(participacao);

                // Atualizar saldo de sonhos do aluno
                aluno.SaldoSonhos += evento.ValorSonhos;

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