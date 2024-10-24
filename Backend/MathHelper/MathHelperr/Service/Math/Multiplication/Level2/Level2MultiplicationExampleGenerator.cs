using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level2MultiplicationExampleGenerator : IMultiplicationExampleGenerator
{
    private static readonly Random _random = new Random();

    public AlgebraResult Example()
    {
        return GetNumbers();
    }

    private AlgebraResult GetNumbers()
    {
        int a = _random.Next(1, 10);
        int equal = a * _random.Next(1, 10);
        int b = equal / a;
        int result = b;
        return new AlgebraResult
        {
            A = a,
            B = b,
            Equal = equal,
            Result =  [result]
        };
        

    }
}
