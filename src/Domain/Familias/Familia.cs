using w_escolas.Domain._abstractClasses;
using w_escolas.Domain.Escolas;

namespace ken_lo.Domain.Familias;

public class Familia : Entity
{
    public Guid EscolaId { get; private set; }
    public string Nome { get; private set; }

    virtual public Escola? Escola { get; private set; }

    public Familia(Guid escolaId, string nome)
    {
        EscolaId = escolaId;
        Nome = nome;
    }
    public void Alterar(string nome)
    {
        Nome = nome;
    }
}
