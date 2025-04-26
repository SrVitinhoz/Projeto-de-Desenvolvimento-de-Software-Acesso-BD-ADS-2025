public class Funcionario
{
    public int IdFuncionario { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Cargo { get; set; }   REM ENUM: professor, administrativo
    public string Status { get; set; } REM ENUM: ativo, demitido
}
