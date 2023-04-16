using Bogus;
using FluentAssertions;
using w_escolas.Domain.TiposDeCursos;

namespace kendo_londrina_test;

public class UnitTest1
{
    [Fact(DisplayName = nameof(TipoDeCursoTest))]
    public void TipoDeCursoTest()
    {
        var faker = new Faker("pt_BR");
        var escolaId = new Guid();
        var codigo = faker.Random.AlphaNumeric(5);
        var nome = faker.Commerce.Department();
        var ordem = faker.Random.Int(1, 100);

        var tipoDeCurso = new TipoDeCurso(
            escolaId,
            codigo,
            nome,
            ordem
        );

        tipoDeCurso.Should().NotBeNull();
        tipoDeCurso.EscolaId.Should().Be(escolaId);
        tipoDeCurso.Codigo.Should().Be(codigo);
        tipoDeCurso.Nome.Should().Be(nome);
        tipoDeCurso.Ordem.Should().Be(ordem);
    }}