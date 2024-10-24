using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level3MultiplicationExampleGenerator : IMultiplicationExampleGenerator
{
    private static readonly Random _random = new Random();

    public AlgebraResult Example()
    {
        return GetNumbers();
    }

    private AlgebraResult GetNumbers()
    {
        int a = _random.Next(1, 10);
        int b = _random.Next(1, 21);
        int result = a * b;
        return new AlgebraResult
        {
            A = a,
            B = b,
            Result =  [result]
        };
        

    }
}
