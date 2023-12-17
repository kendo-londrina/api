using ken_lo.Domain;
using ken_lo.Domain.Repository;

namespace ken_lo.Application.UseCases;
public class AlunoInsert
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlunoInsert(
        IAlunoRepository alunoRepository,
        IUnitOfWork unitOfWork)
    {
        _alunoRepository = alunoRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<AlunoInsertOutput> Handle(
        AlunoInsertInput input,
        CancellationToken cancellationToken)
    {
        var aluno = new Aluno(
            input.EscolaId,
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
        await _alunoRepository.Insert(aluno, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return new AlunoInsertOutput(
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
