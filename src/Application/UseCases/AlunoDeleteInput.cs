namespace ken_lo.Application.UseCases;
public class AlunoDeleteInput
{
    public Guid Id { get; set; }
    public AlunoDeleteInput(
        Guid id
    ) {
        Id = id;
    }
}
