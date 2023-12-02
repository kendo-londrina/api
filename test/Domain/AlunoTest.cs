using Bogus;
using FluentAssertions;
using ken_lo.Domain.Enderecos;
using ken_lo.Domain.Validation;
using w_escolas.Domain.Enderecos;

namespace ken_lo.Domain;

[Collection(nameof(AlunoFixture))]
public class AlunoTest
{
    private readonly AlunoFixture _alunoFixture;
    public AlunoTest(AlunoFixture alunoFixture)
        => _alunoFixture = alunoFixture;

    [Fact()]
    public void Instanciar()
    {
        var validData = _alunoFixture.GetValidObject();

        var aluno = new Aluno(
            validData.EscolaId,
            validData.Nome,
            validData.Codigo,
            validData.DataNascimento,
            validData.Nacionalidade,
            validData.UfNascimento,
            validData.CidadeNascimento,
            validData.Sexo,
            validData.Rg,
            validData.Cpf,
            validData.Email,
            validData.TelCelular,
            validData.Religiao
        );

        aluno.Should().NotBeNull();
        aluno.EscolaId.Should().Be(validData.EscolaId);
        aluno.Codigo.Should().Be(validData.Codigo);
        aluno.Nome.Should().Be(validData.Nome);
        aluno.DataNascimento.Should().Be(validData.DataNascimento);
        aluno.Nacionalidade.Should().Be(validData.Nacionalidade);
        aluno.UfNascimento.Should().Be(validData.UfNascimento);
        aluno.CidadeNascimento.Should().Be(validData.CidadeNascimento);
        aluno.Sexo.Should().Be(validData.Sexo);
        aluno.Rg.Should().Be(validData.Rg);
        aluno.Cpf.Should().Be(validData.Cpf);
        aluno.Email.Should().Be(validData.Email);
        aluno.TelCelular.Should().Be(validData.TelCelular);
        aluno.Religiao.Should().Be(validData.Religiao);
    }

    [Theory()]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void NomeNaoNuloOuVazio(string nome)
    {
        var validData = _alunoFixture.GetValidObject();

        Action action = () => new Aluno(
            validData.EscolaId,
            nome,
            validData.Codigo,
            validData.DataNascimento,
            validData.Nacionalidade,
            validData.UfNascimento,
            validData.CidadeNascimento,
            validData.Sexo,
            validData.Rg,
            validData.Cpf,
            validData.Email,
            validData.TelCelular,
            validData.Religiao
        );

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Nome não pode ser nulo ou vazio", exception.Message);
    }

    [Fact()]
    public void Alterar() {
        var validData = _alunoFixture.GetValidObject();
        var aluno = new Aluno(
            validData.EscolaId,
            validData.Nome,
            validData.Codigo,
            validData.DataNascimento,
            validData.Nacionalidade,
            validData.UfNascimento,
            validData.CidadeNascimento,
            validData.Sexo,
            validData.Rg,
            validData.Cpf,
            validData.Email,
            validData.TelCelular,
            validData.Religiao
        );

        var dadosAlterados = _alunoFixture.GetValidObject();

        aluno.Alterar(
            dadosAlterados.Nome,
            dadosAlterados.Codigo,
            dadosAlterados.DataNascimento,
            dadosAlterados.Nacionalidade,
            dadosAlterados.UfNascimento,
            dadosAlterados.CidadeNascimento,
            dadosAlterados.Sexo,
            dadosAlterados.Rg,dadosAlterados.Cpf,
            dadosAlterados.Email,
            dadosAlterados.TelCelular,
            dadosAlterados.Religiao
        );

        aluno.Should().NotBeNull();
        aluno.Nome.Should().Be(dadosAlterados.Nome);
        aluno.Codigo.Should().Be(dadosAlterados.Codigo);
        aluno.DataNascimento.Should().Be(dadosAlterados.DataNascimento);
        aluno.Nacionalidade.Should().Be(dadosAlterados.Nacionalidade);
        aluno.UfNascimento.Should().Be(dadosAlterados.UfNascimento);
        aluno.CidadeNascimento.Should().Be(dadosAlterados.CidadeNascimento);
        aluno.Sexo.Should().Be(dadosAlterados.Sexo);
        aluno.Rg.Should().Be(dadosAlterados.Rg);
        aluno.Cpf.Should().Be(dadosAlterados.Cpf);
        aluno.Email.Should().Be(dadosAlterados.Email);
        aluno.TelCelular.Should().Be(dadosAlterados.TelCelular);
        aluno.Religiao.Should().Be(dadosAlterados.Religiao);
    }

    [Fact()]
    public void AlterarEndereco() {

        var faker = new Faker("pt_BR");
        var endereco = new Endereco(
            faker.Address.ZipCode(),
            faker.Address.StateAbbr(),
            faker.Address.City(),
            faker.Address.County(),
            faker.Address.County(),
            faker.Address.SecondaryAddress(),
            faker.Address.FullAddress(),
            faker.Random.EnumValues<TipoDeEndereco>().ToString()
        );

        var aluno = _alunoFixture.GetValidObject();        
        aluno.AlterarEndereco(endereco);

        aluno.Should().NotBeNull();
        aluno.Endereco.Should().Be(endereco);
    }

}
