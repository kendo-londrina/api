namespace ken_lo.Domain.Repository;

public interface IAlunoRepository
{
    public Task Insert(Aluno aluno, CancellationToken cancellationToken);
    public Task<Aluno> Get(Guid id, CancellationToken cancellationToken);
    public Task Delete(Aluno aluno, CancellationToken cancellationToken);
}
