using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.TiposDeCursos;
public class TipoDeCursoTest
{
    [Fact()]
    public void InstanciarObjeto()
    {
        var faker = new Faker("pt_BR");
        var escolaId = new Guid();
        var codigo = faker.Random.AlphaNumeric(10);
        var nome = faker.Commerce.Department();
        var ordem = faker.Random.Int(1, 100);

        var tipoDeCurso = new TipoDeCurso(
            escolaId, codigo, nome, ordem
        );

        tipoDeCurso.Should().NotBeNull();
        tipoDeCurso.EscolaId.Should().Be(escolaId);
        tipoDeCurso.Codigo.Should().Be(codigo);
        tipoDeCurso.Nome.Should().Be(nome);
        tipoDeCurso.Ordem.Should().Be(ordem);
    }

    [Fact()]
    public void Alterar()
    {
        var faker = new Faker("pt_BR");

        var tipoDeCurso = new TipoDeCurso(
            new Guid(),
            faker.Random.AlphaNumeric(10),
            faker.Commerce.Department(),
            faker.Random.Int(1, 100)
        );

        var codigoAlterado = faker.Random.AlphaNumeric(10);
        var nomeAlterado = faker.Commerce.Department();
        var ordemAlterado = faker.Random.Int(1, 100);

        tipoDeCurso.Alterar(codigoAlterado, nomeAlterado, ordemAlterado);

        tipoDeCurso.Should().NotBeNull();
        tipoDeCurso.Codigo.Should().Be(codigoAlterado);
        tipoDeCurso.Nome.Should().Be(nomeAlterado);
        tipoDeCurso.Ordem.Should().Be(ordemAlterado);
    }
    
}