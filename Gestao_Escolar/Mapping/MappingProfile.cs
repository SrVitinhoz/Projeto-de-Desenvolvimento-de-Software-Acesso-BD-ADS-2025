using AutoMapper;
using GestaoEscolar.DTOs;
using GestaoEscolar.Models;

namespace GestaoEscolar.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Aluno mappings
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => src.Turma != null ? src.Turma.Nome : null));
            CreateMap<AlunoCreateDTO, Aluno>();
            CreateMap<AlunoUpdateDTO, Aluno>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Turma mappings
            CreateMap<Turma, TurmaDTO>()
                .ForMember(dest => dest.FuncionarioNome, opt => opt.MapFrom(src => src.Funcionario != null ? src.Funcionario.Nome : null));
            CreateMap<TurmaCreateDTO, Turma>();
            CreateMap<TurmaUpdateDTO, Turma>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Funcionario mappings
            CreateMap<Funcionario, FuncionarioDTO>();
            CreateMap<FuncionarioCreateDTO, Funcionario>();
            CreateMap<FuncionarioUpdateDTO, Funcionario>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Materia mappings
            CreateMap<Materia, MateriaDTO>()
                .ForMember(dest => dest.FuncionarioNome, opt => opt.MapFrom(src => src.Funcionario != null ? src.Funcionario.Nome : null))
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => src.Turma != null ? src.Turma.Nome : null));
            CreateMap<MateriaCreateDTO, Materia>();
            CreateMap<MateriaUpdateDTO, Materia>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Chamada mappings
            CreateMap<Chamada, ChamadaDTO>()
                .ForMember(dest => dest.MateriaNome, opt => opt.MapFrom(src => src.Materia != null ? src.Materia.Nome : null))
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => src.Turma != null ? src.Turma.Nome : null));
            CreateMap<ChamadaCreateDTO, Chamada>();
            CreateMap<ChamadaUpdateDTO, Chamada>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ChamadaAluno mappings
            CreateMap<ChamadaAluno, ChamadaAlunoDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.DataChamada, opt => opt.MapFrom(src => src.Chamada != null ? src.Chamada.Data : default));
            CreateMap<ChamadaAlunoCreateDTO, ChamadaAluno>();
            CreateMap<ChamadaAlunoUpdateDTO, ChamadaAluno>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Evento mappings
            CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.FuncionarioNome, opt => opt.MapFrom(src => src.Funcionario != null ? src.Funcionario.Nome : null));
            CreateMap<EventoCreateDTO, Evento>();
            CreateMap<EventoUpdateDTO, Evento>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ParticipacaoEvento mappings
            CreateMap<ParticipacaoEvento, ParticipacaoEventoDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.EventoNome, opt => opt.MapFrom(src => src.Evento != null ? src.Evento.Nome : null))
                .ForMember(dest => dest.DataEvento, opt => opt.MapFrom(src => src.Evento != null ? src.Evento.DataInicio : default));
            CreateMap<ParticipacaoEventoCreateDTO, ParticipacaoEvento>();
            CreateMap<ParticipacaoEventoUpdateDTO, ParticipacaoEvento>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // HistoricoSonhos mappings
            CreateMap<HistoricoSonhos, HistoricoSonhosDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null));
            CreateMap<HistoricoSonhosCreateDTO, HistoricoSonhos>();
            CreateMap<HistoricoSonhosUpdateDTO, HistoricoSonhos>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // TransferenciaTurma mappings
            CreateMap<TransferenciaTurma, TransferenciaTurmaDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.TurmaOrigemNome, opt => opt.MapFrom(src => src.TurmaOrigem != null ? src.TurmaOrigem.Nome : null))
                .ForMember(dest => dest.TurmaDestinoNome, opt => opt.MapFrom(src => src.TurmaDestino != null ? src.TurmaDestino.Nome : null));
            CreateMap<TransferenciaTurmaCreateDTO, TransferenciaTurma>();
            CreateMap<TransferenciaTurmaUpdateDTO, TransferenciaTurma>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Matricula mappings
            CreateMap<Matricula, MatriculaDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => src.Turma != null ? src.Turma.Nome : null));
            CreateMap<MatriculaCreateDTO, Matricula>();
            CreateMap<MatriculaUpdateDTO, Matricula>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}