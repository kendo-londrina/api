using ken_lo.Domain.Repository;

namespace ken_lo.Application.UseCases;
public class AlunoGet
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoGet(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }
    public async Task<AlunoGetOutput> Handle(
        AlunoGetInput input,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.Get(input.Id, cancellationToken);
        return AlunoGetOutput.FromAluno(aluno);
    }
}
