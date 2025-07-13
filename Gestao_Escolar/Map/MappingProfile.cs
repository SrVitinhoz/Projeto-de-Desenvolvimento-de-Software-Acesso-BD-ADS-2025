using AutoMapper;
using Gestao_Escolar.Models;
using Gestao_Escolar.DTOs;
using System;

namespace Gestao_Escolar
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Turma
            CreateMap<Turma, TurmaDTO>();
            CreateMap<TurmaCreateDTO, Turma>();
            CreateMap<TurmaUpdateDTO, Turma>();

            // Materia
            CreateMap<Materia, MateriaDTO>();
            CreateMap<MateriaCreateDTO, Materia>();
            CreateMap<MateriaUpdateDTO, Materia>();

            // Funcionario
            CreateMap<Funcionario, FuncionarioDTO>()
                .ForMember(dest => dest.MateriaNome, opt => opt.MapFrom(src => src.Materia != null ? src.Materia.Nome : null));
            CreateMap<FuncionarioCreateDTO, Funcionario>();
            CreateMap<FuncionarioUpdateDTO, Funcionario>()
                .ForMember(dest => dest.Senha, opt => opt.Ignore());

            /// Aluno - MAPEAMENTO CORRIGIDO COM TOLERÂNCIA A CASE
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => src.Turma != null ? src.Turma.Nome : null))
                .ForMember(dest => dest.StatusMatricula, opt => opt.MapFrom(src => src.StatusMatricula.ToString()))
                .ForMember(dest => dest.Periodo, opt => opt.MapFrom(src => src.Periodo.ToString()));

            CreateMap<AlunoCreateDTO, Aluno>()
                .ForMember(dest => dest.SaldoSonhos, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.NumMatricula, opt => opt.Ignore())
                .ForMember(dest => dest.StatusMatricula, opt => opt.MapFrom(src =>
                    ParseStatusMatricula(src.StatusMatricula)))
                .ForMember(dest => dest.Periodo, opt => opt.MapFrom(src =>
                    ParsePeriodoTurma(src.Periodo)));

            CreateMap<AlunoUpdateDTO, Aluno>()
                .ForMember(dest => dest.StatusMatricula, opt => opt.MapFrom(src =>
                    ParseStatusMatricula(src.StatusMatricula)))
                .ForMember(dest => dest.Periodo, opt => opt.MapFrom(src =>
                    ParsePeriodoTurma(src.Periodo)));

            // Evento
            CreateMap<Evento, EventoDTO>();
            CreateMap<EventoCreateDTO, Evento>();
            CreateMap<EventoUpdateDTO, Evento>();

            // ParticipacaoEvento
            CreateMap<ParticipacaoEvento, ParticipacaoEventoDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.EventoNome, opt => opt.MapFrom(src => src.Evento != null ? src.Evento.Nome : null));
            CreateMap<ParticipacaoEventoCreateDTO, ParticipacaoEvento>();
            CreateMap<ParticipacaoEventoUpdateDTO, ParticipacaoEvento>();

            // Chamada
            CreateMap<Chamada, ChamadaDTO>()
                .ForMember(dest => dest.FuncionarioNome, opt => opt.MapFrom(src => src.Funcionario != null ? src.Funcionario.Nome : null))
                .ForMember(dest => dest.TurmaNome, opt => opt.MapFrom(src => src.Turma != null ? src.Turma.Nome : null))
                .ForMember(dest => dest.MateriaNome, opt => opt.MapFrom(src => src.Materia != null ? src.Materia.Nome : null));
            CreateMap<ChamadaCreateDTO, Chamada>();
            CreateMap<ChamadaUpdateDTO, Chamada>();

            // ChamadaAluno
            CreateMap<ChamadaAluno, ChamadaAlunoDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null));
            CreateMap<ChamadaAlunoCreateDTO, ChamadaAluno>();
            CreateMap<ChamadaAlunoUpdateDTO, ChamadaAluno>();

            // HistoricoSonhos
            CreateMap<HistoricoSonhos, HistoricoSonhosDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.FuncionarioNome, opt => opt.MapFrom(src => src.Funcionario != null ? src.Funcionario.Nome : null));
            CreateMap<HistoricoSonhosCreateDTO, HistoricoSonhos>();
            CreateMap<HistoricoSonhosUpdateDTO, HistoricoSonhos>();

            // TransferenciaTurma
            CreateMap<TransferenciaTurma, TransferenciaTurmaDTO>()
                .ForMember(dest => dest.AlunoNome, opt => opt.MapFrom(src => src.Aluno != null ? src.Aluno.Nome : null))
                .ForMember(dest => dest.TurmaOrigemNome, opt => opt.MapFrom(src => src.TurmaOrigem != null ? src.TurmaOrigem.Nome : null))
                .ForMember(dest => dest.TurmaDestinoNome, opt => opt.MapFrom(src => src.TurmaDestino != null ? src.TurmaDestino.Nome : null))
                .ForMember(dest => dest.FuncionarioNome, opt => opt.MapFrom(src => src.Funcionario != null ? src.Funcionario.Nome : null));
            CreateMap<TransferenciaTurmaCreateDTO, TransferenciaTurma>();
            CreateMap<TransferenciaTurmaUpdateDTO, TransferenciaTurma>();




        }

        private static StatusMatricula ParseStatusMatricula(string status)
        {
            return status?.ToLower() switch
            {
                "ativo" => StatusMatricula.Ativo,
                "ativa" => StatusMatricula.Ativo, // Aceita variação
                "desligado" => StatusMatricula.Desligado,
                "inativo" => StatusMatricula.Inativa,
                "inativa" => StatusMatricula.Inativa, // Aceita variação
                _ => StatusMatricula.Ativo // Valor padrão
            };
        }

        private static PeriodoTurma ParsePeriodoTurma(string periodo)
        {
            return periodo?.ToLower() switch
            {
                "matutino" => PeriodoTurma.Matutino,
                "vespertino" => PeriodoTurma.Vespertino,
                _ => PeriodoTurma.Matutino // Valor padrão
            };
        }
    }
}