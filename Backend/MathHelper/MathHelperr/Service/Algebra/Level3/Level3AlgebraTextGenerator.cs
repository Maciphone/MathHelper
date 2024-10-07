using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level3AlgebraTextGenerator : IAlgebraTextGenerator
{
    private readonly Random _random = new Random();
    public string Answer(AlgebraResult example)
    {
        var a = example.A;
        var b = example.B;
        var equal = example.Equal;
        var rnd = _random.Next(0, 3);
        
        string result = rnd switch
        {
            0 => $"? + {a} + {b} = {equal}",   
            1 => $"{a} + ? + {b} = {equal}",   
            2 => $"{a} + {b} + ? = {equal}",

            _ => throw new ArgumentOutOfRangeException()
        };

        return result;

    }
}