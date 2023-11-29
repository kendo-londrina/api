using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Enderecos;
using w_escolas.Domain.Escolas;

namespace ken_lo.Domain;

public class ResponsavelPeloAluno : Pessoa
{
    public Guid EscolaId { get; private set; }
    virtual public Escola? Escola { get; private set; }
    public Guid? EnderecoId { get; private set; }
    virtual public Endereco? Endereco { get; private set; }

    public ResponsavelPeloAluno(Guid escolaId,
        string nome,
        DateTime? dataNascimento,
        string? nacionalidade,
        string? ufNascimento,
        string? cidadeNascimento,
        string? sexo,
        string? rg,
        string? cpf,
        string? email,
        string? telCelular,
        string? religiao) : base(
            nome, dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email, telCelular, religiao)
    {
        this.EscolaId = escolaId;

        Validate();
    }

    public void Validate()
    {
        if (String.IsNullOrWhiteSpace(Nome))
            throw new EntityValidationException($"{nameof(Nome)} não pode ser nulo ou vazio");
        if (String.IsNullOrWhiteSpace(Cpf))
            throw new EntityValidationException($"{nameof(Cpf)} não pode ser nulo ou vazio");
    }
}
