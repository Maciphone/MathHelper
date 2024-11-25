using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level2AlgebraTextGenerator : IAlgebraTextGenerator
{
    public int Level => 2;
    private readonly Random _random = new Random();
    public string Answer(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        var rnd = _random.Next(0, 2);
        return rnd == 0 ? $"{a} + ? = {b}" : $"? + {a} = {b}";
    }
}