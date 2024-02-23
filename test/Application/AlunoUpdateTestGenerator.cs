using ken_lo.Application.UseCases;

namespace ken_lo.Application;
public class AlunoUpdateTestGenerator
{
    protected AlunoUpdateTestGenerator() { }
    public static IEnumerable<Object[]> GetAlunosToUpdate(int times = 10) {
        var fixture = new AlunoUpdateFixture();
        for (int i = 0; i < times; i++)
        {
            var example = fixture.GetExample();
            var input = fixture.GetInput(example.Id);
            yield return new object[] {
                example, input
            };
        }
    }
}