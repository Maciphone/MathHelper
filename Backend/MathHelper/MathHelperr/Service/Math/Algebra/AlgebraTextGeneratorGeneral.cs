using MathHelperr.Model;

namespace MathHelperr.Service;

public class AlgebraTextGeneratorGeneral
    : IAlgebraTextGenerator
{
    private readonly Random _random = new Random();
    public int Level => 1;
    
    public string Answer(AlgebraResult example)
    {
        switch (example.Level)
        {
            case 1:
                return Level1(example);
            case 2:
                return Level2(example);
            case 3:
                return Level3(example);
            default:
                return Level1(example);
        }
    }

    public string Level1(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} + {b} = ?";
    }

    public string Level2(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        var rnd = _random.Next(0, 2);
        return rnd == 0 ? $"{a} + ? = {b}" : $"? + {a} = {b}";
    }

    public string Level3(AlgebraResult example)
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