using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.Escolas;

public class EscolaTest
{
    [Fact(DisplayName = nameof(InstanciarObjetoSimples))]
    public void InstanciarObjetoSimples()
    {
        var faker = new Faker("pt_BR");
        var nomeFantasia = faker.Company.CompanyName();
        var uf = faker.Address.State();
        var cidade = faker.Address.City();

        var escola = new Escola(
            nomeFantasia,
            uf,
            cidade
          );

        escola.Should().NotBeNull();
        escola.NomeFantasia.Should().Be(nomeFantasia);
        escola.Uf.Should().Be(uf);
        escola.Cidade.Should().Be(cidade);
    }

    [Fact(DisplayName = nameof(InstanciarObjetoCompleto))]
    public void InstanciarObjetoCompleto() {
        var faker = new Faker("pt_BR");
        var nomeFantasia = faker.Company.CompanyName();
        var uf = faker.Address.State();
        var cidade = faker.Address.City();
        var cnpj = "12345678000199";
        var razaoSocial = faker.Company.CompanyName();
        var cep = faker.Address.ZipCode();
        var bairro = faker.Address.County();
        var endereco = faker.Address.FullAddress();
        var telefone = faker.Phone.PhoneNumber();
        var email = faker.Internet.Email();
        var website = faker.Internet.Url();

        var escola = new Escola(
            nomeFantasia,
            uf,
            cidade,
            cnpj,
            razaoSocial,
            cep,
            bairro,
            endereco,
            telefone,
            email,
            website
          );

        escola.Should().NotBeNull();
        escola.NomeFantasia.Should().Be(nomeFantasia);
        escola.Uf.Should().Be(uf);
        escola.Cnpj.Should().Be(cnpj);
        escola.RazaoSocial.Should().Be(razaoSocial);
        escola.Cep.Should().Be(cep);
        escola.Bairro.Should().Be(bairro);
        escola.Endereco.Should().Be(endereco);
        escola.Telefone.Should().Be(telefone);
        escola.Email.Should().Be(email);
        escola.Website.Should().Be(website);
    }

    [Fact(DisplayName = nameof(AlterarDados))]
    public void AlterarDados() {
        var faker = new Faker("pt_BR");

        var escola = new Escola(
            faker.Company.CompanyName(),
            faker.Address.State(),
            faker.Address.City(),
            "12345678000199",
            faker.Company.CompanyName(),
            faker.Address.ZipCode(),
            faker.Address.County(),
            faker.Address.FullAddress(),
            faker.Phone.PhoneNumber(),
            faker.Internet.Email(),
            faker.Internet.Url()
        );

        var nomeFantasiaAlterado = faker.Company.CompanyName();
        var ufAlterado = faker.Address.State();
        var cidadeAlterado = faker.Address.City();
        var cnpjAlterado = "12345678000199";
        var razaoSocialAlterado = faker.Company.CompanyName();
        var cepAlterado = faker.Address.ZipCode();
        var bairroAlterado = faker.Address.County();
        var enderecoAlterado = faker.Address.FullAddress();
        var telefoneAlterado = faker.Phone.PhoneNumber();
        var emailAlterado = faker.Internet.Email();
        var websiteAlterado = faker.Internet.Url();

        escola.Alterar(
            nomeFantasiaAlterado,
            ufAlterado,
            cidadeAlterado,
            cnpjAlterado,
            razaoSocialAlterado,
            cepAlterado,
            bairroAlterado,
            enderecoAlterado,
            telefoneAlterado,
            emailAlterado,
            websiteAlterado
        );

        escola.Should().NotBeNull();
        escola.NomeFantasia.Should().Be(nomeFantasiaAlterado);
        escola.Uf.Should().Be(ufAlterado);
        escola.Cnpj.Should().Be(cnpjAlterado);
        escola.RazaoSocial.Should().Be(razaoSocialAlterado);
        escola.Cep.Should().Be(cepAlterado);
        escola.Bairro.Should().Be(bairroAlterado);
        escola.Endereco.Should().Be(enderecoAlterado);
        escola.Telefone.Should().Be(telefoneAlterado);
        escola.Email.Should().Be(emailAlterado);
        escola.Website.Should().Be(websiteAlterado);
    }
}