public class HistoricoSonhos
{
    public int IdHistoricoSonhos { get; set; }
    public int AlunoId { get; set; }
    public DateTime Data { get; set; }
    public string Tipo { get; set; }       REM ENUM: adicao, subtracao, uso_evento
    public string? Motivo { get; set; }
    public int Valor { get; set; }
    public int? FuncionarioId { get; set; }
}
