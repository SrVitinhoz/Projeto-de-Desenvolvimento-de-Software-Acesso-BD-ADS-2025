public class Evento
{
    public int IdEvento { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public int ValorSonhos { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Status { get; set; } REM ENUM: ativo, encerrado
}
