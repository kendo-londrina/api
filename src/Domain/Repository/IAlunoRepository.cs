namespace ken_lo.Domain.Repository;

public interface IAlunoRepository
{
    public Task Inserir(Aluno aluno, CancellationToken cancellationToken);
    public Task<Aluno> Get(Guid id, CancellationToken cancellationToken);
}
