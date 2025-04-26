public class Aluno
{
    public int IdAluno { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? FotoUrl { get; set; }
    public int? TurmaId { get; set; }
    public string StatusMatricula { get; set; } //REM ENUM: ativo, inativo
    public int SaldoSonhos { get; set; }
}
