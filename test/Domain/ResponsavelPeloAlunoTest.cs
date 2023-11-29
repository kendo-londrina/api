using FluentAssertions;

namespace ken_lo.Domain;

[Collection(nameof(ResponsavelPeloAlunoFixture))]
public class ResponsavelPeloAlunoTest
{
    private readonly ResponsavelPeloAlunoFixture _responsavelPeloAlunoFixture;
    public ResponsavelPeloAlunoTest(ResponsavelPeloAlunoFixture responsavelPeloAlunoFixture)
        => _responsavelPeloAlunoFixture = responsavelPeloAlunoFixture;
    
    [Fact()]
    public void ObjetoInstanciar()
    {
        var validData = _responsavelPeloAlunoFixture.GetValidObject();

        var responsavelPeloAluno = new ResponsavelPeloAluno(
            validData.EscolaId,
            validData.Nome,
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

        responsavelPeloAluno.Should().NotBeNull();
        responsavelPeloAluno.EscolaId.Should().Be(validData.EscolaId);
        responsavelPeloAluno.Nome.Should().Be(validData.Nome);
        responsavelPeloAluno.DataNascimento.Should().Be(validData.DataNascimento);
        responsavelPeloAluno.Nacionalidade.Should().Be(validData.Nacionalidade);
        responsavelPeloAluno.UfNascimento.Should().Be(validData.UfNascimento);
        responsavelPeloAluno.CidadeNascimento.Should().Be(validData.CidadeNascimento);
        responsavelPeloAluno.Sexo.Should().Be(validData.Sexo);
        responsavelPeloAluno.Rg.Should().Be(validData.Rg);
        responsavelPeloAluno.Cpf.Should().Be(validData.Cpf);
        responsavelPeloAluno.Email.Should().Be(validData.Email);
        responsavelPeloAluno.TelCelular.Should().Be(validData.TelCelular);
        responsavelPeloAluno.Religiao.Should().Be(validData.Religiao);
    }

    [Theory()]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void NomeNaoNuloOuVazio(string? nome)
    {
        var validData = _responsavelPeloAlunoFixture.GetValidObject();

        Action action = () => new ResponsavelPeloAluno(
            validData.EscolaId,
            nome!,
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

    [Theory()]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void CPFNaoNuloOuVazio(string? cpf)
    {
        var validData = _responsavelPeloAlunoFixture.GetValidObject();

        Action action = () => new ResponsavelPeloAluno(
            validData.EscolaId,
            validData.Nome,
            validData.DataNascimento,
            validData.Nacionalidade,
            validData.UfNascimento,
            validData.CidadeNascimento,
            validData.Sexo,
            validData.Rg,
            cpf,
            validData.Email,
            validData.TelCelular,
            validData.Religiao
        );

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Cpf não pode ser nulo ou vazio", exception.Message);
    }

    [Fact()]
    public void ObjetoAlterar()
    {
        var validData = _responsavelPeloAlunoFixture.GetValidObject();
        var responsavel = new ResponsavelPeloAluno(
            validData.EscolaId,
            validData.Nome,
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

        var dadosAlterados = _responsavelPeloAlunoFixture.GetValidObject();
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
