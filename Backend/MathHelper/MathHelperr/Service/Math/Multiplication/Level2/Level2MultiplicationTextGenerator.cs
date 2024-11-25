using MathHelperr.Model;

namespace MathHelperr.Service;

public class Level2MultiplicationTextGenerator : IMultiplicationTextGenerator
{
    public int Level => 2;
    private static readonly Random _random = new Random();

    public string Answer(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        var equal = example.Equal;
        int rnd = _random.Next(0, 2);
        
        return rnd ==0 ?  $"{a} * ? = {equal}" : $"? * {a} = {equal}";
    }
}