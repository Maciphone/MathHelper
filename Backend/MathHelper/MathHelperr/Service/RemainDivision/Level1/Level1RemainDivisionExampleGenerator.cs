using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level1RemainDivisionExampleGenerator : IRemainDivisonExampleGenerator
{
    private static readonly Random _random = new Random();

    public AlgebraResult Example()
    {
        return GetNumbers();
    }

    private AlgebraResult GetNumbers()
    {
        int a = _random.Next(1, 10);
        int b = _random.Next(1, 10)*a + _random.Next(1,a+1);
        int result = b%a;
        return new AlgebraResult
        {
            A = b,
            B = a,
            Result = result
        };
        

    }
}
