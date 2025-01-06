using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level3AlgebraExampleGenerator : IAlgebraExampleGenerator
{
    private static readonly Random _random = new Random();
    public int Level => 3;

    public AlgebraResult Example()
    {
        return GetNumbers();
    }

    private AlgebraResult GetNumbers()
    {
        int equal = _random.Next(1, 100);
        int a = GetANumber(equal);
        int b = GetANumber(equal- a);
        int result = equal - a - b;
        return new AlgebraResult
        {
            A = a,
            B = b,
            Equal = equal,
            Result =  [result],
            Level = Level
        };
        

    }

    private int GetANumber(int max)
    {
        return _random.Next(1, max);
    }
}

