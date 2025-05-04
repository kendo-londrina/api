using ken_lo.Domain.Repository;

namespace ken_lo.Application.UseCases.Aluno;
public class AlunoUpdate
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlunoUpdate(
        IAlunoRepository alunoRepository,
        IUnitOfWork unitOfWork)
    {
        _alunoRepository = alunoRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<AlunoUpdateOutput> Handle(
        AlunoUpdateInput input,
        CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.Get(input.Id, cancellationToken);        
        aluno.Alterar(
            input.Nome,
            input.Codigo,
            input.DataNascimento,
            input.Nacionalidade,
            input.UfNascimento,
            input.CidadeNascimento,
            input.Sexo,
            input.Rg,
            input.Cpf,
            input.Email,
            input.TelCelular,
            input.Religiao
        );
        await _alunoRepository.Update(aluno, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return new AlunoUpdateOutput(
            aluno.Id,
            aluno.EscolaId,
            aluno.Nome,
            aluno.Codigo,
            aluno.DataNascimento,
            aluno.Nacionalidade,
            aluno.UfNascimento,
            aluno.CidadeNascimento,
            aluno.Sexo,
            aluno.Rg,
            aluno.Cpf,
            aluno.Email,
            aluno.TelCelular,
            aluno.Religiao
        );
    }
}
