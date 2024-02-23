using ken_lo.Domain;

namespace ken_lo.Application.UseCases;
public class AlunoGetOutput
{
    public Guid Id { get; set; }
    public Guid EscolaId { get; set; }
    public string? Codigo { get; set; }
    public string Nome { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Nacionalidade { get; set; }
    public string? UfNascimento { get; set; }
    public string? CidadeNascimento { get; set; }
    public string? Sexo { get; set; }
    public string? Rg { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? TelCelular { get; set; }
    public string? Religiao { get; set; }

    public AlunoGetOutput(
        Guid id,
        Guid escolaId,
        string nome,
        string? codigo,
        DateTime? dataNascimento,
        string? nacionalidade,
        string? ufNascimento,
        string? cidadeNascimento,
        string? sexo,
        string? rg,
        string? cpf,
        string? email,
        string? telCelular,
        string? religiao)
    {
        Id = id;
        EscolaId = escolaId;
        Nome = nome;
        Codigo = codigo;
        DataNascimento = dataNascimento;
        Nacionalidade = nacionalidade;
        UfNascimento = ufNascimento;
        CidadeNascimento = cidadeNascimento;
        Sexo = sexo;
        Rg = rg;
        Cpf = cpf;
        Email = email;
        TelCelular = telCelular;
        Religiao = religiao;
    }

    public static AlunoGetOutput FromAluno(Aluno aluno)
    {
        return new AlunoGetOutput(
            aluno.Id,
            aluno.EscolaId,
            aluno.Nome,
            aluno.Codigo,
            aluno.DataNascimento,
            aluno.Nacionalidade,
            aluno.UfNascimento,
            aluno.CidadeNascimento,
            aluno.Sexo,
            aluno.Rg,
            aluno.Cpf,
            aluno.Email,
            aluno.TelCelular,
            aluno.Religiao
        );
    }
}
