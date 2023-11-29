using Bogus;
using FluentAssertions;

namespace ken_lo.Domain;

public class ResponsavelPeloAlunoTest
{
    [Fact()]
    public void ObjetoInstanciar()
    {
        var faker = new Faker("pt_BR");

        var escolaId = Guid.NewGuid();
        var nome = faker.Person.FullName;
        var dataNascimento = faker.Date.Past();
        var nacionalidade = faker.Address.Country();
        var ufNascimento = faker.Address.StateAbbr();
        var cidadeNascimento = faker.Address.City();
        var sexo = faker.Person.Gender.ToString();
        var rg = faker.Random.AlphaNumeric(15);
        var cpf = faker.Random.AlphaNumeric(11);
        var email = faker.Internet.Email();
        var telCelular = faker.Phone.PhoneNumber();
        var religiao = faker.Name.FullName();

        var responsavelPeloAluno = new ResponsavelPeloAluno(
            escolaId, nome,
            dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email, telCelular, religiao
        );

        responsavelPeloAluno.Should().NotBeNull();
        responsavelPeloAluno.EscolaId.Should().Be(escolaId);
        responsavelPeloAluno.Nome.Should().Be(nome);
        responsavelPeloAluno.DataNascimento.Should().Be(dataNascimento);
        responsavelPeloAluno.Nacionalidade.Should().Be(nacionalidade);
        responsavelPeloAluno.UfNascimento.Should().Be(ufNascimento);
        responsavelPeloAluno.CidadeNascimento.Should().Be(cidadeNascimento);
        responsavelPeloAluno.Sexo.Should().Be(sexo);
        responsavelPeloAluno.Rg.Should().Be(rg);
        responsavelPeloAluno.Cpf.Should().Be(cpf);
        responsavelPeloAluno.Email.Should().Be(email);
        responsavelPeloAluno.TelCelular.Should().Be(telCelular);
        responsavelPeloAluno.Religiao.Should().Be(religiao);
    }

    [Theory()]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void NomeNaoNuloOuVazio(string? nome)
    {
        var escolaId = Guid.NewGuid();

        Action action = () => new ResponsavelPeloAluno(
            escolaId, nome!,
            null, null, null, null,
            null, null, null, null, null, null
        );
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Nome não pode ser nulo ou vazio", exception.Message);
    }

    [Theory()]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void CPFNaoNuloOuVazio(string? cpf)
    {
        var faker = new Faker("pt_BR");

        var escolaId = Guid.NewGuid();
        var nome = faker.Person.FullName;

        Action action = () => new ResponsavelPeloAluno(
            escolaId, nome,
            null, null, null, null,
            null, null, cpf, null, null, null
        );
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Cpf não pode ser nulo ou vazio", exception.Message);
    }

    [Fact()]
    public void ObjetoAlterar()
    {
        var faker = new Faker("pt_BR");

        var escolaId = Guid.NewGuid();
        var nome = faker.Person.FullName;
        var dataNascimento = faker.Date.Past();
        var nacionalidade = faker.Address.Country();
        var ufNascimento = faker.Address.StateAbbr();
        var cidadeNascimento = faker.Address.City();
        var sexo = faker.Person.Gender.ToString();
        var rg = faker.Random.AlphaNumeric(15);
        var cpf = faker.Random.AlphaNumeric(11);
        var email = faker.Internet.Email();
        var telCelular = faker.Phone.PhoneNumber();
        var religiao = faker.Name.FullName();

        var responsavel = new ResponsavelPeloAluno(
            escolaId, nome,
            dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email, telCelular, religiao
        );

        var dadosAlterados = new {
            Nome = faker.Person.FullName,
            DataNascimento = faker.Date.Past(),
            Nacionalidade = faker.Address.Country(),
            UfNascimento = faker.Address.StateAbbr(),
            CidadeNascimento = faker.Address.City(),
            Sexo = faker.Person.Gender.ToString(),
            Rg = faker.Random.AlphaNumeric(15),
            Cpf = faker.Random.AlphaNumeric(11),
            Email = faker.Internet.Email(),
            TelCelular = faker.Phone.PhoneNumber(),
            Religiao = faker.Name.FullName(),
        };

        responsavel.Alterar(
            dadosAlterados.Nome,
            dadosAlterados.DataNascimento,
            dadosAlterados.Nacionalidade,
            dadosAlterados.UfNascimento,
            dadosAlterados.CidadeNascimento,
            dadosAlterados.Sexo,
            dadosAlterados.Rg,
            dadosAlterados.Cpf,
            dadosAlterados.Email,
            dadosAlterados.TelCelular,
            dadosAlterados.Religiao
        );

        Assert.Equal(responsavel.Nome, dadosAlterados.Nome);
        Assert.Equal(responsavel.DataNascimento, dadosAlterados.DataNascimento);
        Assert.Equal(responsavel.Nacionalidade, dadosAlterados.Nacionalidade);
        Assert.Equal(responsavel.UfNascimento, dadosAlterados.UfNascimento);
        Assert.Equal(responsavel.CidadeNascimento, dadosAlterados.CidadeNascimento);
        Assert.Equal(responsavel.Sexo, dadosAlterados.Sexo);
        Assert.Equal(responsavel.Rg, dadosAlterados.Rg);
        Assert.Equal(responsavel.Cpf, dadosAlterados.Cpf);
        Assert.Equal(responsavel.Email, dadosAlterados.Email);
        Assert.Equal(responsavel.TelCelular, dadosAlterados.TelCelular);
        Assert.Equal(responsavel.Religiao, dadosAlterados.Religiao);
    }

}
