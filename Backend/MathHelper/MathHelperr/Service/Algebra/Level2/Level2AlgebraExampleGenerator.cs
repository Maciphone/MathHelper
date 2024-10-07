using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level2AlgebraExampleGenerator : IAlgebraExampleGenerator
{
    private static readonly Random _random = new Random();

    public AlgebraResult Example()
    {
        return GetNumbers();
    }

    private AlgebraResult GetNumbers()
    {
        int a = _random.Next(1, 90);
        int equal = _random.Next(a, 100);
        int result = equal-a;
        return new AlgebraResult
        {
            A = a,
            B = equal,
            Result = result
        };
        

    }

   
}

