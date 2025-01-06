using MathHelperr.Model;

namespace MathHelperr.Service;

public class MultiplicationTextGeneratorGeneral
    : IMultiplicationTextGenerator
{
    public int Level => 1;
    private static readonly Random _random = new Random();
   
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
    


    private string Level1(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} * {b} = ?";
    }

    private string Level2(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        var equal = example.Equal;
        int rnd = _random.Next(0, 2);
        
        return rnd ==0 ?  $"{a} * ? = {equal}" : $"? * {a} = {equal}";
    }

    private string Level3(AlgebraResult example)
    {
        int a = example.A;
        int b = example.B;
        return $"{a} * {b} = ?";
    }
}