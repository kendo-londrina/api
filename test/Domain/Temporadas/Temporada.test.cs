using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.Temporadas;

public class TemporadaTest
{
    [Fact()]
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

        var temporada = new Temporada(
            escolaId, codigo, nome,
            ano, semestre, quadrimestre, trimestre, bimestre, mes
        );

        temporada.Should().NotBeNull();
        temporada.EscolaId.Should().Be(escolaId);
        temporada.Codigo.Should().Be(codigo);
        temporada.Nome.Should().Be(nome);
        temporada.Ano.Should().Be(ano);
        temporada.Semestre.Should().Be(semestre);
        temporada.Quadrimestre.Should().Be(quadrimestre);
        temporada.Trimestre.Should().Be(trimestre);
        temporada.Bimestre.Should().Be(bimestre);
        temporada.Mes.Should().Be(mes);
    }

    [Fact()]
    public void Alterar()
    {
        var faker = new Faker("pt_BR");

        var temporada = new Temporada(
            new Guid(),
            faker.Random.AlphaNumeric(5),
            faker.Commerce.Department(),
            faker.Random.Int(1980, 2100),
            faker.Random.Int(1, 2),
            faker.Random.Int(1, 3),
            faker.Random.Int(1, 4),
            faker.Random.Int(1, 6),
            faker.Random.Int(1, 12)
        );

        var codigoAlterado = faker.Random.AlphaNumeric(5);
        var nomeAlterado = faker.Commerce.Department();
        var anoAlterado = faker.Random.Int(1980, 2100);
        var semestreAlterado = faker.Random.Int(1, 2);
        var quadrimestreAlterado = faker.Random.Int(1, 3);
        var trimestreAlterado = faker.Random.Int(1, 4);
        var bimestreAlterado = faker.Random.Int(1, 6);
        var mesAlterado = faker.Random.Int(1, 12);

        temporada.Alterar(codigoAlterado, nomeAlterado
            , anoAlterado, semestreAlterado, quadrimestreAlterado
            , trimestreAlterado,bimestreAlterado, mesAlterado
        );

        temporada.Should().NotBeNull();
        temporada.Codigo.Should().Be(codigoAlterado);
        temporada.Nome.Should().Be(nomeAlterado);
        temporada.Ano.Should().Be(anoAlterado);
        temporada.Semestre.Should().Be(semestreAlterado);
        temporada.Quadrimestre.Should().Be(quadrimestreAlterado);
        temporada.Trimestre.Should().Be(trimestreAlterado);
        temporada.Bimestre.Should().Be(bimestreAlterado);
        temporada.Mes.Should().Be(mesAlterado);
    }
}
