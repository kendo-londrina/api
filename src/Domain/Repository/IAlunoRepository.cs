using ken_lo.Domain;

namespace ken_lo.Domain.Repository;

public interface IAlunoRepository
{
    public Task Inserir(Aluno aluno, CancellationToken cancellationToken);
}
