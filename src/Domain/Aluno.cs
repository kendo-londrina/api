using ken_lo.Domain._abstractClasses;
using ken_lo.Domain.Validation;
using w_escolas.Domain.Enderecos;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Matriculas;

namespace ken_lo.Domain;

public class Aluno : Pessoa
{
    public Guid EscolaId { get; private set; }
    virtual public Escola? Escola { get; private set; }
    public string? Codigo { get; private set; }
    public Guid? EnderecoId { get; private set; }

    virtual public Endereco? Endereco { get; private set; }
    virtual public IEnumerable<Matricula>? Matriculas { get; private set; }

    public Aluno(Guid escolaId,
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
        string? religiao) : base(
            nome, dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email,telCelular, religiao)
    {
        EscolaId = escolaId;
        Codigo = codigo;
        Validate();
    }

    public void Alterar(
        string nome,
        string? codigo = null,
        DateTime? dataNascimento = null,
        string? nacionalidade = null,
        string? ufNascimento = null,
        string? cidadeNascimento = null,
        string? sexo = null,
        string? rg = null,
        string? cpf = null,
        string? email = null,
        string? telCelular = null,
        string? religiao = null)
    {
        Nome = nome;
        Codigo = codigo ?? Codigo;
        DataNascimento = dataNascimento ?? DataNascimento;
        Nacionalidade = nacionalidade ?? Nacionalidade;
        UfNascimento = ufNascimento ?? UfNascimento;
        CidadeNascimento = cidadeNascimento ?? CidadeNascimento;
        Sexo = sexo ?? Sexo;
        Rg = rg ?? Rg;
        Cpf = cpf ?? Cpf;
        Email = email ?? Email;
        TelCelular = telCelular ?? TelCelular;
        Religiao = religiao ?? Religiao;
        Validate();
    }

    public void AlterarEndereco(Endereco endereco)
    {
        Endereco = endereco;
    }

    public void Validate()
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        // validar:
        // - Nome deve ser no mínimo: nome + sobrenome
        //     nome e sobrenome(s) devem ter mais de 2 letras
        // - Data de nascimento: o atleta deve ser maior de 5 anos
        // - se informado Email deve estar em formato válido email@dominio.suf
        // - se informado Celular deve estar em formato válido (99)9-9999-9999
        // - se informado Cpf deve ser válido
        // ...
    }    
}
