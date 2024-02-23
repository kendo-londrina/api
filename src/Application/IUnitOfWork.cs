namespace ken_lo.Application;
public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}