using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level1DivisionExampleGenerator : IDivisionExampleGenerator
{
    private static readonly Random _random = new Random();

    public AlgebraResult Example()
    {
        return GetNumbers();
    }

    private AlgebraResult GetNumbers()
    {
        int a = _random.Next(1, 11);
        int b = _random.Next(1, 11);
        int result = a ;
        return new AlgebraResult
        {
            A = a*b,
            B = b,
            Result =  [result]
        };
        

    }
}
