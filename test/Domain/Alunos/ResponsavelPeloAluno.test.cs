using Bogus;
using FluentAssertions;
using ken_lo.Domain;
using ken_lo.Domain.Alunos;

namespace len_lo.Domain.Alunos;

public class ResponsavelPeloAlunoTest
{
    [Fact()]
    public void InstanciarObjetoCompleto()
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

}
