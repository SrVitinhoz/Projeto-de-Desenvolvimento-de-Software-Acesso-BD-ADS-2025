public class TransferenciaTurma
{
    public int IdTransferenciaTurma { get; set; }
    public int AlunoId { get; set; }
    public int? TurmaOrigemId { get; set; }
    public int? TurmaDestinoId { get; set; }
    public DateTime DataTransferencia { get; set; }
    public int? FuncionarioId { get; set; }
}
