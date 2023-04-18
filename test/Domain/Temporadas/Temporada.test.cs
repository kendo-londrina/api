using Bogus;
using FluentAssertions;
using w_escolas.Domain.Temporadas;

namespace kendo_londrina_test.Domain.Temporadas;

public class TemporadaTest
{
    [Fact(DisplayName = nameof(InstanciarObjeto))]
    public void InstanciarObjeto()
    {
        var faker = new Faker("pt_BR");
        var escolaId = new Guid();
        var codigo = faker.Random.AlphaNumeric(5);
        var nome = faker.Commerce.Department();
        var ano = faker.Random.Int(1980, 2100);
        var semestre = faker.Random.Int(1, 2);
        var quadrimestre = faker.Random.Int(1, 3);
        var trimestre = faker.Random.Int(1, 4);
        var bimestre = faker.Random.Int(1, 6);
        var mes = faker.Random.Int(1, 12);

        var tipoDeCurso = new Temporada(
            escolaId, codigo, nome,
            ano, semestre, quadrimestre, trimestre, bimestre, mes
        );

        tipoDeCurso.Should().NotBeNull();
        tipoDeCurso.EscolaId.Should().Be(escolaId);
        tipoDeCurso.Codigo.Should().Be(codigo);
        tipoDeCurso.Nome.Should().Be(nome);
        tipoDeCurso.Ano.Should().Be(ano);
        tipoDeCurso.Semestre.Should().Be(semestre);
        tipoDeCurso.Quadrimestre.Should().Be(quadrimestre);
        tipoDeCurso.Trimestre.Should().Be(trimestre);
        tipoDeCurso.Bimestre.Should().Be(bimestre);
        tipoDeCurso.Mes.Should().Be(mes);
    }}