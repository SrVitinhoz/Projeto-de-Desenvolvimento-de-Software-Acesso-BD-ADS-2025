public class Matricula
{
    public int IdMatricula { get; set; }
    public int AlunoId { get; set; }
    public int AnoLetivo { get; set; }
    public string Status { get; set; }    REM ENUM: ativa, cancelada, transferida
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int? FuncionarioId { get; set; }
}
