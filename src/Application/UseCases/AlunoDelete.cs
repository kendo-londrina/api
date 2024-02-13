using ken_lo.Domain;
using ken_lo.Domain.Repository;

namespace ken_lo.Application.UseCases;
public class AlunoDelete
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlunoDelete(
        IAlunoRepository alunoRepository,
        IUnitOfWork unitOfWork)
    {
        _alunoRepository = alunoRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(
        AlunoDeleteInput input,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.Get(input.Id, cancellationToken);
        await _alunoRepository.Delete(aluno, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}
