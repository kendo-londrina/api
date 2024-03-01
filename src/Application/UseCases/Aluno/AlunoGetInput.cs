namespace ken_lo.Application.UseCases.Aluno;
public class AlunoGetInput
{
    public Guid Id { get; set; }
    public AlunoGetInput(Guid id)
    {
        Id = id;
    }
}
