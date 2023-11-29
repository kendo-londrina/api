using ken_lo.Domain._abstractClasses;
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

    public void Alterar(
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
        string? religiao)
    {
        this.Nome = nome;
        this.DataNascimento = dataNascimento;
        this.Nacionalidade = nacionalidade;
        this.UfNascimento = ufNascimento;
        this.CidadeNascimento = cidadeNascimento;
        this.Sexo = sexo;
        this.Rg = rg;
        this.Cpf = cpf;
        this.Email = email;
        this.TelCelular = telCelular;
        this.Religiao = religiao;
        Validate();
    }

    public void Validate()
    {
        if (String.IsNullOrWhiteSpace(Nome))
            throw new EntityValidationException($"{nameof(Nome)} não pode ser nulo ou vazio");
        if (String.IsNullOrWhiteSpace(Cpf))
            throw new EntityValidationException($"{nameof(Cpf)} não pode ser nulo ou vazio");
        // validar:
        // - CPF válido
        // - Nome deve ser no mínimo: nome + sobrenome
        //     nome e sobrenome(s) devem ter mais de 2 letras
        // - Data de nascimento: responsável deve ser maior de 18
        // - se informado email deve estar em formato válido email@dominio.suf
        // - se informado celular deve estar em formato válido (99)9-9999-9999
        // ...
    }
}
